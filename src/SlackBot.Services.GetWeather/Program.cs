using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SlackBot.Services.GetWeather.Infrastructure.NServiceBus;
using System.IO;
using System.Threading.Tasks;

namespace SlackBot.Services.GetWeather
{
    public class Program
    {
        public static async Task Main()
        {
            IConfiguration configuration = GetConfiguration(new ConfigurationBuilder());

            await CreateHostBuilder(configuration).RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder(IConfiguration configuration) =>
            Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(builder => builder.AddConfiguration(configuration))
                .ConfigureNServiceBus(configuration);

        public static IConfiguration GetConfiguration(IConfigurationBuilder builder) =>
            builder
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .AddEnvironmentVariables()
                .Build();
    }
}
