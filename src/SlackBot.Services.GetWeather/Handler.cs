using NServiceBus;
using SlackBot.Common.Commands;
using System.Threading.Tasks;

namespace SlackBot.Services.GetWeather
{
    public class Handler : IHandleMessages<GetWeatherCommand>
    {
        public Task Handle(GetWeatherCommand message, IMessageHandlerContext context)
        {
            return Task.CompletedTask;
        }
    }
}
