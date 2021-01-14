using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Parsing;
using System.Linq;

namespace SlackBot.WebHooks.Infrastructure.System.CommandLine
{
    public static class Extensions
    {
        public static IServiceCollection AddSlackCommands(this IServiceCollection services, Type assemblyType)
        {
            Type commandType = typeof(Command);
            IEnumerable<Type> commands = assemblyType.Assembly.GetExportedTypes()
                .Where(command => commandType.IsAssignableFrom(command));

            foreach (Type command in commands)
            {
                services.AddScoped(commandType, command);
            }

            services.AddScoped<Parser>(serviceProvider => BuildParser(serviceProvider));

            return services;
        }

        private static Parser BuildParser(IServiceProvider serviceProvider)
        {
            CommandLineBuilder commandLineBuilder = new CommandLineBuilder();
            IEnumerable<Command> commands = serviceProvider.GetServices<Command>();

            foreach (Command command in commands)
            {
                commandLineBuilder.AddCommand(command);
            }

            return commandLineBuilder.UseDefaults().Build();
        }
    }
}
