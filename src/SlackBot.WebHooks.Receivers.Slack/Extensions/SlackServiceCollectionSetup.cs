using Microsoft.AspNetCore.WebHooks.Metadata;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SlackBot.WebHooks.Receivers.Slack.Filters;
using SlackBot.WebHooks.Receivers.Slack.Metadata;
using System;

namespace SlackBot.WebHooks.Receivers.Slack.Extensions
{
    internal static class SlackServiceCollectionSetup
    {
        public static void AddSlackServices(IServiceCollection services)
        {
            _ = services ?? throw new ArgumentNullException(nameof(services));

            WebHookMetadata.Register<SlackMetadata>(services);
            services.TryAddSingleton<SlackVerifySignatureFilter>();
        }
    }
}
