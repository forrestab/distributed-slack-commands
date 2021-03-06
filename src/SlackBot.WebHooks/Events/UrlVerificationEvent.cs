using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Events
{
    public class UrlVerificationEvent
    {
        public class Request : IRequest<Result>
        {
            public string Challenge { get; set; }
            public string Token { get; set; }
        }

        public class Result
        {
            public string Challenge { get; set; }
        }

        public class Handler : IRequestHandler<Request, Result>
        {
            private readonly ILogger<Handler> logger;

            public Handler(ILogger<Handler> logger)
            {
                this.logger = logger;
            }

            public async Task<Result> Handle(Request request, CancellationToken cancellationToken)
            {
                this.logger.LogDebug("UrlVerification request: {Token}, {Challenge}", request.Token, request.Challenge);

                return new Result { Challenge = request.Challenge };
            }
        }
    }
}
