using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NServiceBus;

namespace SlackBot.WebHooks.Infrastructure.NServiceBus
{
    public static class Extensions
    {
        public static IServiceCollection AddNServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            NServiceBusOptions options = configuration.GetSection("NServiceBus").Get<NServiceBusOptions>();
            EndpointConfiguration endpointConfig = new EndpointConfiguration(options.EndpointName);

            endpointConfig.EnableInstallers();
            endpointConfig
                .UseTransport<RabbitMQTransport>()
                .ConnectionString(new ConnectionFactory()
                {
                    Host = options.Host,
                    Port = options.Port,
                    VirtualHost = options.VirtualHost,
                    UserName = options.UserName,
                    Password = options.Password
                }.ToString())
                .UseConventionalRoutingTopology();

            services.AddSingleton<IEndpointInstance>(Endpoint.Start(endpointConfig).GetAwaiter().GetResult());

            return services;
        }
    }
}
