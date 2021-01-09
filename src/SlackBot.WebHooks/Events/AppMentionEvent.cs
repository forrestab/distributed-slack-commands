using MediatR;
using SlackBot.WebHooks.Events.Common;
using System;
using System.Collections.Generic;
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
                return Unit.Value;
            }
        }
    }
}
