using System;
using System.Linq;
using CampaignManagement.Core;
using EcommerceSample.Data.Contracts;
using OrderManagement.Core;
using OrderManagement.Core.Events;
using SharedKernel;
using SharedKernel.Contracts;

namespace ApplicationServices.Orders.Handlers
{
    public class CampaignStockCheckHandler : IHandler<NewProductOrderCreatedEvent>
    {
        private readonly IRepository<Sale> _saleRepository;
        private readonly IRepository<Campaign> _campaignRepository;
        public Type Type => typeof(NewProductOrderCreatedEvent);

        public CampaignStockCheckHandler(IRepository<Sale> saleRepository, IRepository<Campaign> campaignRepository, SubscriptionManager subscriptionManager)
        {
            _saleRepository = saleRepository;
            _campaignRepository = campaignRepository;
            subscriptionManager.AddSubscription<NewProductOrderCreatedEvent, CampaignStockCheckHandler>();
        }

        public void Handle(NewProductOrderCreatedEvent domainEvent)
        {
            var sales = _saleRepository.GetAll(x => x.CampaignName == domainEvent.ProductOrder.CampaignName);
            var campaign = _campaignRepository.Get(x => x.Name == domainEvent.ProductOrder.CampaignName);
            if (campaign != null && sales.Any())
            {
                var totalSales = sales.Sum(x => x.Quantity);
                if (campaign.TargetSalesCount == totalSales)
                {
                    campaign = campaign.ChangeStatus(Status.Passive);
                    _campaignRepository.Update(campaign);
                }
            }
        }
    }
}