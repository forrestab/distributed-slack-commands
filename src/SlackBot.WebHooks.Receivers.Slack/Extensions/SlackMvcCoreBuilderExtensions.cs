using Microsoft.Extensions.DependencyInjection;
using System;

namespace SlackBot.WebHooks.Receivers.Slack.Extensions
{
    public static class SlackMvcCoreBuilderExtensions
    {
        public static IMvcBuilder AddSlackWebHooks(this IMvcBuilder builder)
        {
            _ = builder ?? throw new ArgumentNullException(nameof(builder));

            SlackServiceCollectionSetup.AddSlackServices(builder.Services);

            return builder
                .AddWebHooks();
        }
    }
}
