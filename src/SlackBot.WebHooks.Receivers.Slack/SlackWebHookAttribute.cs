using Microsoft.AspNetCore.WebHooks;
using Microsoft.AspNetCore.WebHooks.Metadata;
using System;

namespace SlackBot.WebHooks.Receivers.Slack
{
    public class SlackWebHookAttribute : WebHookAttribute, IWebHookEventSelectorMetadata
    {
        private string eventName;

        public string EventName
        {
            get { return this.eventName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Value cannot be null or empty.", nameof(value));
                }

                this.eventName = value;
            }
        }

        public SlackWebHookAttribute()
            : base(SlackConstants.ReceiverName)
        { }
    }
}
