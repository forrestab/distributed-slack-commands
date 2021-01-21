using NServiceBus;

namespace SlackBot.Common.Commands
{
    public class GetWeatherCommand : IEvent
    {
        public int Zip { get; set; }
    }
}
