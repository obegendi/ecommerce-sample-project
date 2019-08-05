using CommandHandlers.Contracts;
using SharedKernel;
using SharedKernel.Contracts;

namespace CommandHandlers.CommandStrategies
{
    public class DefaultCommandStrategy : ICommandStrategy
    {
        private readonly IConsoleWriter _consoleWriter;
        public DefaultCommandStrategy(IConsoleWriter consoleWriter)
        {
            _consoleWriter = consoleWriter;

        }

        /// <inheritdoc />
        public string Command { get; set; }
        /// <inheritdoc />
        public void Execute(string[] commands)
        {
            _consoleWriter.Write("Command is not valid!");
        }
    }
}
