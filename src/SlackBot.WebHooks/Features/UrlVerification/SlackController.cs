using Microsoft.AspNetCore.Mvc;
using SlackBot.WebHooks.Receivers.Slack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Features.UrlVerification
{
    public partial class SlackController : Controller
    {
        [SlackWebHook]
        public async Task<IActionResult> OnUrlVerification(string id, string @event, UrlVerificationRequest data)
        {
            return this.Ok(new UrlVerificationResponse { Challenge = data.Challenge });
        }
    }
}
