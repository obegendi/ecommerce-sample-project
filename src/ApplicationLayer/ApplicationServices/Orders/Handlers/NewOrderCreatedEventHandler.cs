using System;
using EcommerceSample.Data.Contracts;
using OrderManagement.Core;
using OrderManagement.Core.Events;
using ProductManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;

namespace ApplicationServices.Orders.Handlers
{
    public class NewOrderCreatedEventHandler : IHandler<NewProductOrderCreatedEvent>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Sale> _saleRepository;
        public NewOrderCreatedEventHandler(IRepository<Sale> saleRepository, IRepository<Product> productRepository, SubscriptionManager subscriptionManager)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            subscriptionManager.AddSubscription<NewProductOrderCreatedEvent, NewOrderCreatedEventHandler>();
        }
        public Type Type => typeof(NewProductOrderCreatedEvent);

        /// <inheritdoc />
        public void Handle(NewProductOrderCreatedEvent domainEvent)
        {
            _saleRepository.Add(new Sale
            {
                ProductCode = domainEvent.ProductOrder.ProductCode,
                TotalPaid = domainEvent.ProductOrder.TotalAmountWithDiscount,
                Quantity = domainEvent.ProductOrder.Quantity,
                CampaignName = domainEvent.ProductOrder.CampaignName
            });
            _productRepository.Update(domainEvent.ProductOrder.OrderedProduct);
        }
    }

}
