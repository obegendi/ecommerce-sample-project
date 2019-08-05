using ApplicationServices.Orders.Validation;
using CampaignManagement.Core;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using OrderManagement.Core;
using ProductManagement.Core;
using ValidationModule;

namespace ApplicationServices.Orders
{
    public class OrderServices : IOrderServices
    {
        private readonly IRepository<Campaign> _campaignRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderServices(
            IRepository<Order> orderRepository,
            IRepository<Product> productRepository,
            IRepository<Campaign> campaignRepository,
            IUnitOfWork unitOfWork)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
        }


        /// <inheritdoc />
        public void CreateOrder(string productCode, int quantity)
        {
            var productToOrder = _productRepository.Get(x => x.ProductCode == productCode);
            var context = new ValidateContext(new ValidateProductAndStock(null));
            context.DoValidation(productToOrder, quantity);
            var result = context.HandleResults();
            if (!result)
                return;

            var campaign = _campaignRepository.Get(x => x.ProductCode == productCode && x.Status == Status.Active);

            var order = new Order().Create(quantity, productCode);
            _orderRepository.Add(order);
            var productOrder = new ProductOrder();
            productOrder = productOrder.Create(productToOrder, order, campaign);
            _unitOfWork.Commit();
        }
    }

}
