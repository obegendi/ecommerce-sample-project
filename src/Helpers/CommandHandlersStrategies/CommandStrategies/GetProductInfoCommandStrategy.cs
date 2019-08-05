using System;
using ApplicationServices.Products;
using CommandHandlers.Contracts;

namespace CommandHandlers.CommandStrategies
{
    public class GetProductInfoCommandStrategy : ICommandStrategy
    {
        private readonly IProductServices _productServices;

        private string _productCode = string.Empty;

        public GetProductInfoCommandStrategy(IProductServices productServices)
        {
            _productServices = productServices;

        }
        /// <inheritdoc />
        public string Command { get; set; } = "get_product_info";

        /// <inheritdoc />
        public void Execute(string[] commands)
        {
            if (commands.Length != 2)
            {
                Console.WriteLine("Command parameters not valid!");
                return;
            }
            _productCode = commands[1];
            _productServices.GetProductInfo(_productCode);
        }
    }
}
