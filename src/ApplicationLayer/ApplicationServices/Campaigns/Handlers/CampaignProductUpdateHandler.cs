using System;
using CampaignManagement.Core;
using CampaignManagement.Core.Events;
using EcommerceSample.Data.Contracts;
using ProductManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;

namespace ApplicationServices.Campaigns.Handlers
{
    public class CampaignProductUpdateHandler : IHandler<UpdateCampaignCurrentManipulationEvent>
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Campaign> _campaignRepository;
        public Type Type => typeof(UpdateCampaignCurrentManipulationEvent);

        public CampaignProductUpdateHandler(IRepository<Product> productRepository, IRepository<Campaign> campaignRepository, SubscriptionManager subscriptionManager)
        {
            _productRepository = productRepository;
            _campaignRepository = campaignRepository;
            subscriptionManager.AddSubscription<UpdateCampaignCurrentManipulationEvent, CampaignProductUpdateHandler>();
        }

        public void Handle(UpdateCampaignCurrentManipulationEvent domainEvent)
        {
            var productCode = domainEvent.Campaign.ProductCode;
            var product = _productRepository.Get(x => x.ProductCode == productCode);
            var campaign = _campaignRepository.Get(x => x.Status == Status.Active && x.ProductCode == productCode);

            product.DiscountedPrice = product.OriginalPrice - (campaign.CurrentPriceManipulation * product.OriginalPrice) / 100;
            _productRepository.Update(product);
        }
    }
}