using NServiceBus;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Events.Commands
{
    public class GetWeatherCommand : Command
    {
        private readonly IEndpointInstance endpoint;

        public GetWeatherCommand(IEndpointInstance endpoint)
            : base("weather", "Check weather for provided zipcode.")
        {
            this.endpoint = endpoint;

            this.AddOption(new Option<int>(new string[] { "--zip", "-z" })
            {
                IsRequired = true
            });
            this.Handler = CommandHandler.Create<GetWeatherOptions>(HandleCommand);
        }

        private async Task HandleCommand(GetWeatherOptions options) => await this.endpoint.Publish(options);
    }

    public class GetWeatherOptions : IEvent
    {
        public int Zip { get; }

        public GetWeatherOptions(int zip)
        {
            this.Zip = zip;
        }
    }
}
