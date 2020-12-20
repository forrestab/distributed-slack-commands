using Microsoft.AspNetCore.WebHooks.Metadata;
using SlackBot.WebHooks.Receivers.Slack.Filters;

namespace SlackBot.WebHooks.Receivers.Slack.Metadata
{
    public class SlackMetadata : WebHookMetadata, IWebHookEventFromBodyMetadata, IWebHookFilterMetadata
    {
        private readonly SlackVerifySignatureFilter verifySignatureFilter;

        public override WebHookBodyType BodyType => WebHookBodyType.Json;

        public bool AllowMissing => false;

        public string BodyPropertyPath => SlackConstants.EventBodyPropertyPath;

        public SlackMetadata(SlackVerifySignatureFilter verifySignatureFilter)
            : base(SlackConstants.ReceiverName)
        {
            this.verifySignatureFilter = verifySignatureFilter;
        }

        public void AddFilters(WebHookFilterMetadataContext context)
        {
            context.Results.Add(this.verifySignatureFilter);
        }
    }
}
