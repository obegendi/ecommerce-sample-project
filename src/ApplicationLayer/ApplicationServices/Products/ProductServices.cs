using CampaignManagement.Core;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using ProductManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;

namespace ApplicationServices.Products
{
    public class ProductServices : IProductServices
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(IRepository<Product> productRepository, IUnitOfWork unitOfWork, IConsoleWriter consoleWriter)
        {
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
            _consoleWriter = consoleWriter;
        }

        /// <inheritdoc />
        public void AddNewProduct(string productCode, int price, int stock)
        {
            var product = new Product();
            product.Create(productCode, price, stock);
            _productRepository.Add(new Product
            {
                Stock = stock,
                OriginalPrice = price,
                DiscountedPrice = price,
                ProductCode = productCode
            });
            _unitOfWork.Commit();
        }

        /// <inheritdoc />
        public void GetProductInfo(string productCode)
        {
            var entity = _productRepository.Get(x => x.ProductCode == productCode);
            _consoleWriter.Write($"Product {entity.ProductCode} info; price {entity.DiscountedPrice}, stock {entity.Stock}");
        }
    }

}
