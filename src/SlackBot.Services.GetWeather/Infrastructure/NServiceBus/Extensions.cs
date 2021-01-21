using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NServiceBus;

namespace SlackBot.Services.GetWeather.Infrastructure.NServiceBus
{
    public static class Extensions
    {
        public static IHostBuilder ConfigureNServiceBus(this IHostBuilder builder, IConfiguration configuration) =>
            builder
                .UseNServiceBus(hostBuilderContext =>
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

                    return endpointConfig;
                });
    }
}
