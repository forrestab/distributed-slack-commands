using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SlackBot.WebHooks.Receivers.Slack;
using System;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Controllers
{
    public class WebHookController : Controller
    {
        protected readonly IMediator mediator;

        public WebHookController(IMediator mediator) => this.mediator = mediator;

        protected async Task<IActionResult> SendAsync(string @event, JToken data) =>
            this.Ok(await this.mediator.Send(JsonConvert.DeserializeObject(data.ToString(), this.GetEventType(@event, data))));

        private Type GetEventType(string @event, JToken request) =>
            @event switch
            {
                SlackEvents.URL_VERIFICATION => typeof(Events.UrlVerificationEvent.Command),
                SlackEvents.APP_RATE_LIMITED => typeof(Events.AppRateLimitedEvent.Request),
                SlackEvents.EVENT_CALLBACK => this.GetEventSubtype(request.SelectToken("$.event.type").Value<string>()),
                _ => throw new InvalidOperationException($"Unknown event: '{@event}'")
            };

        private Type GetEventSubtype(string subtype) =>
            subtype switch
            {
                "app_mention" => typeof(Events.Common.EventCallback<Events.AppMentionEvent.Command>),
                _ => throw new InvalidOperationException($"Unknown subtype event: '{subtype}'")
            };
    }
}
