using System.CommandLine;
using System.CommandLine.Invocation;

namespace SlackBot.WebHooks.Events.Commands
{
    public class GetWeatherCommand : Command
    {
        public GetWeatherCommand()
            : base("weather", "Check weather for provided zipcode.")
        {
            this.AddOption(new Option<int>(new string[] { "--zip", "-z" })
            {
                IsRequired = true
            });
            this.Handler = CommandHandler.Create<GetWeatherOptions>(HandleCommand);
        }

        private void HandleCommand(GetWeatherOptions options)
        {
            
        }
    }

    public class GetWeatherOptions
    {
        public int Zip { get; }

        public GetWeatherOptions(int zip)
        {
            this.Zip = zip;
        }
    }
}
