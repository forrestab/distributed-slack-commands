using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SlackBot.WebHooks.Receivers.Slack;
using System;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Controllers
{
    public class WebHookController : Controller
    {
        protected readonly IMediator mediator;

        public WebHookController(IMediator mediator) => this.mediator = mediator;

        protected async Task<IActionResult> SendAsync(string @event, string data) =>
            this.Ok(await this.mediator.Send(JsonConvert.DeserializeObject(data, this.GetEventType(@event))));

        private Type GetEventType(string @event) =>
            @event switch
            {
                SlackEvents.URL_VERIFICATION => typeof(SlackBot.WebHooks.Events.UrlVerificationEvent.Command),
                SlackEvents.APP_RATE_LIMITED => typeof(SlackBot.WebHooks.Events.AppRateLimitedEvent.Request),
                _ => throw new InvalidOperationException($"Unknown event: '{@event}'")
            };
    }
}
