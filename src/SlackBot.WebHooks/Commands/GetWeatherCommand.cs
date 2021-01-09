using CommandLine;

namespace SlackBot.WebHooks.Commands
{
    [Verb("weather")]
    public class GetWeatherCommand
    {
        [Option('z', "zip", Required = true)]
        public int Zip { get; set; }
    }
}
