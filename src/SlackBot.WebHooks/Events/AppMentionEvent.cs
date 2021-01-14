using MediatR;
using NServiceBus;
using SlackBot.WebHooks.Events.Common;
using System.CommandLine.Parsing;
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
            private readonly Parser parser;

            public Handler(Parser parser)
            {
                this.parser = parser;
            }

            public async Task<Unit> Handle(EventCallback<Command> request, CancellationToken cancellationToken)
            {
                await this.parser.InvokeAsync(string.Join(" ", request.Event.Text.Split(" ").Skip(1)));

                return Unit.Value;
            }
        }
    }
}
