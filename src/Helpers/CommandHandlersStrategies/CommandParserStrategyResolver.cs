using System.Collections.Generic;
using System.Linq;
using CommandHandlers.CommandStrategies;
using CommandHandlers.Contracts;
using SharedKernel;
using SharedKernel.Contracts;

namespace CommandHandlers
{
    public class CommandParserStrategyResolver : ICommandParserStrategyResolver
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IEnumerable<ICommandStrategy> _strategies;

        public CommandParserStrategyResolver(IEnumerable<ICommandStrategy> strategies, IConsoleWriter consoleWriter)
        {
            _strategies = strategies;
            _consoleWriter = consoleWriter;
        }

        public ICommandStrategy Resolver(string[] commands)
        {
            if (!commands.Any() || string.IsNullOrEmpty(commands[0]))
                return new DefaultCommandStrategy(_consoleWriter);
            var commandMethod = _strategies.FirstOrDefault(x => x.Command == commands[0]);
            if (commandMethod is null)
                return new DefaultCommandStrategy(_consoleWriter);
            return commandMethod;
        }
    }

}
