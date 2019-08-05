using System;
using ApplicationServices.Time;
using CommandHandlers.Contracts;

namespace CommandHandlers.CommandStrategies
{
    public class IncreaseTimeCommandStrategy : ICommandStrategy
    {
        private readonly ITimeServices _timeServices;

        private int _hour;

        public IncreaseTimeCommandStrategy(ITimeServices timeServices)
        {
            _timeServices = timeServices;
        }

        /// <inheritdoc />
        public string Command { get; set; } = "increase_time";

        /// <inheritdoc />
        public void Execute(string[] commands)
        {
            if (commands.Length != 2)
            {
                Console.WriteLine("Command parameters not valid!");
                return;
            }

            int.TryParse(commands[1], out _hour);
            _timeServices.IncreaseTime(_hour);
        }
    }
}
