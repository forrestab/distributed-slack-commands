using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SlackBot.WebHooks.Receivers.Slack;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Controllers
{
    public class SlackController : WebHookController
    {
        public SlackController(IMediator mediator)
            : base(mediator)
        { }

        [SlackWebHook]
        public async Task<IActionResult> OnSlackEvent(string @event, JRaw data) =>
            await base.SendAsync(@event, data.ToString());
    }
}
