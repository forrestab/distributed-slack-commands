using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace SlackBot.Services.GetWeather
{
    public class Program
    {
        public static async Task Main()
        {
            await CreateHostBuilder().RunConsoleAsync();
        }

        public static IHostBuilder CreateHostBuilder() =>
            Host.CreateDefaultBuilder();
    }
}
