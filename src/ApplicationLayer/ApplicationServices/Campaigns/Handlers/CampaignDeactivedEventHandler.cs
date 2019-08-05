using System;
using CampaignManagement.Core.Events;
using EcommerceSample.Data.Contracts;
using ProductManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;

namespace ApplicationServices.Campaigns.Handlers
{
    public class CampaignDeactivedEventHandler : IHandler<CampaignDeactivedEvent>
    {
        private readonly IRepository<Product> _productRepository;
        public Type Type { get; } = typeof(CampaignDeactivedEvent);

        public CampaignDeactivedEventHandler(IRepository<Product> productRepository, SubscriptionManager subscriptionManager)
        {
            _productRepository = productRepository;
            subscriptionManager.AddSubscription<CampaignDeactivedEvent, CampaignDeactivedEventHandler>();
        }

        public void Handle(CampaignDeactivedEvent domainEvent)
        {
            var product = _productRepository.Get(x => x.ProductCode == domainEvent.Campaign.ProductCode);
            product.DiscountedPrice = product.OriginalPrice;
            _productRepository.Update(product);
        }
    }
}