using System;
using ApplicationServices.Orders;
using CommandHandlers.Contracts;

namespace CommandHandlers.CommandStrategies
{
    public class CreateOrderCommandStrategy : ICommandStrategy
    {
        private readonly IOrderServices _orderServices;
        private string _productCode;
        private int _quantity;

        public CreateOrderCommandStrategy(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        public string Command { get; set; } = "create_order";

        /// <inheritdoc />
        public void Execute(string[] commands)
        {
            if (commands.Length != 3)
            {
                Console.WriteLine("Command parameters not valid!");
                return;
            }
            _productCode = commands[1];
            int.TryParse(commands[2], out _quantity);
            _orderServices.CreateOrder(_productCode, _quantity);
        }
    }
}
