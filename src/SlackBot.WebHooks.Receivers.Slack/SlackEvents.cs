using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Receivers.Slack
{
    public static class SlackEvents
    {
        public const string URL_VERIFICATION = "url_verification";
        public const string APP_RATE_LIMITED = "app_rate_limited";
        public const string EVENT_CALLBACK = "event_callback";
        public const string APP_MENTION = "app_mention";
    }
}
