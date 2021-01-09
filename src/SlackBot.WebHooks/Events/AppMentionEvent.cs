using CommandLine;
using MediatR;
using SlackBot.WebHooks.Commands;
using SlackBot.WebHooks.Events.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SlackBot.WebHooks.Events
{
    public class AppMentionEvent
    {
        public class Command : IRequest
        {
            public string Text { get; set; }
        }

        public class Handler : IRequestHandler<EventCallback<Command>>
        {
            public async Task<Unit> Handle(EventCallback<Command> request, CancellationToken cancellationToken)
            {
                var result = Parser.Default.ParseArguments<GetWeatherCommand>(request.Event.Text.Split(" ").Skip(1));

                return Unit.Value;
            }
        }
    }
}
