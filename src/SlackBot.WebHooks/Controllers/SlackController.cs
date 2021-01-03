using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using SlackBot.WebHooks.Receivers.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Controllers
{
    public class SlackController : Controller
    {
        [SlackWebHook]
        public async Task<IActionResult> OnSlackEvent(string @event, JObject data)
        {
            return this.Ok();
        }
    }
}
