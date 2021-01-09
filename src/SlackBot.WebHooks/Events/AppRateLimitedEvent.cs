using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Events
{
    public class AppRateLimitedEvent
    {
        public class Request : IRequest
        {
            [JsonProperty("minute_rate_limited")]
            public long MinuteRateLimited { get; set; }
        }

        public class Handler : IRequestHandler<Request>
        {
            private readonly ILogger<Handler> logger;

            public Handler(ILogger<Handler> logger)
            {
                this.logger = logger;
            }

            public Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                this.logger.LogDebug("AppRateLimited request: {MinuteRateLimited}", request.MinuteRateLimited);

                return Task.FromResult(Unit.Value);
            }
        }
    }
}
