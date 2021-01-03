using Microsoft.AspNetCore.WebHooks;
using Microsoft.AspNetCore.WebHooks.Metadata;
using System;

namespace SlackBot.WebHooks.Receivers.Slack
{
    public class SlackWebHookAttribute : WebHookAttribute
    {
        public SlackWebHookAttribute()
            : base(SlackConstants.ReceiverName)
        { }
    }
}
