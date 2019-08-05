using System;
using ApplicationServices.Products;
using CommandHandlers.Contracts;

namespace CommandHandlers.CommandStrategies
{
    public class CreateProductCommandStrategy : ICommandStrategy
    {
        private readonly IProductServices _productService;
        private int _price;

        private string _productCode = string.Empty;
        private int _stock;

        public CreateProductCommandStrategy(IProductServices productService)
        {
            _productService = productService;
        }
        public string Command { get; set; } = "create_product";

        /// <inheritdoc />
        public void Execute(string[] commands)
        {
            if (commands.Length != 4)
            {
                Console.WriteLine("Command parameters not valid!");
                return;
            }
            _productCode = commands[1];
            int.TryParse(commands[2], out _price);
            int.TryParse(commands[3], out _stock);

            _productService.AddNewProduct(_productCode, _price, _stock);
        }
    }
}
