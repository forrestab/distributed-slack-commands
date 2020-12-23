using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.WebHooks.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Receivers.Slack.Filters
{
    public class SlackVerifySignatureFilter : WebHookVerifySignatureFilter, IAsyncResourceFilter
    {
        private static readonly char SEPARATOR = '=';

        public override string ReceiverName => SlackConstants.ReceiverName;

        public SlackVerifySignatureFilter(IConfiguration configuration, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
            : base(configuration, hostingEnvironment, loggerFactory)
        { }

        public async Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            _ = context ?? throw new ArgumentNullException(nameof(context));
            _ = next ?? throw new ArgumentNullException(nameof(next));

            HttpRequest request = context.HttpContext.Request;
            if (HttpMethods.IsPost(request.Method))
            {
                IActionResult errorResult = base.EnsureSecureConnection(this.ReceiverName, request);
                if (errorResult != null)
                {
                    context.Result = errorResult;

                    return;
                }

                string headerValue = base.GetRequestHeader(request, SlackConstants.SignatureHeaderName, out errorResult);
                if (errorResult != null)
                {
                    context.Result = errorResult;

                    return;
                }

                string signatureValue = this.GetSignature(SlackConstants.SignatureHeaderName, headerValue,
                    SEPARATOR, SlackConstants.SignatureHeaderKey, out errorResult);
                if (errorResult != null)
                {
                    context.Result = errorResult;

                    return;
                }

                string requestTimestamp = base.GetRequestHeader(request, SlackConstants.TimestampHeaderName, out errorResult);
                if (errorResult != null)
                {
                    context.Result = errorResult;

                    return;
                }

                byte[] expectedHash = base.FromHex(signatureValue, SlackConstants.SignatureHeaderName);
                if (expectedHash == null)
                {
                    context.Result = base.CreateBadHexEncodingResult(SlackConstants.SignatureHeaderName);

                    return;
                }

                string secretKey = base.GetSecretKey(this.ReceiverName, context.RouteData, SlackConstants.SecretKeyMinLength);
                if (secretKey == null)
                {
                    context.Result = new NotFoundResult();

                    return;
                }

                byte[] secret = Encoding.UTF8.GetBytes(secretKey);
                byte[] prefix = Encoding.UTF8.GetBytes($"{SlackConstants.SignatureHeaderKey}:{requestTimestamp}:");
                byte[] actualHash = await base.ComputeRequestBodySha256HashAsync(request, secret, prefix);
                if (!SecretEqual(expectedHash, actualHash))
                {
                    context.Result = base.CreateBadSignatureResult(SlackConstants.SignatureHeaderName);

                    return;
                }
            }

            await next();
        }

        protected virtual string GetSignature(string headerName, string header, char separator, string signatureKey, out IActionResult errorResult)
        {
            string[] tokens = header.Split(separator, StringSplitOptions.TrimEntries);

            if (tokens.Length != 2 || !tokens[0].Equals(signatureKey, StringComparison.OrdinalIgnoreCase))
            {
                base.Logger.LogWarning("Invalid {headerName} header value. Expecting a value of '{signatureKey}=<value>'.",
                    headerName, signatureKey);

                errorResult = new BadRequestObjectResult($"Invalid {headerName} header value. Expecting a value of '{signatureKey}=<value>'.");

                return null;
            }

            errorResult = null;

            return tokens[1];
        }
    }
}
