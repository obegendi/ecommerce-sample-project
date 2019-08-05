using OrderManagement.Core.Events;
using SharedKernel;
using SharedKernel.Contracts;

namespace OrderManagement.Core
{
    public class Sale : Entity, IAggregateRoot
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public int TotalPaid { get; set; }
        public string CampaignName { get; set; }

        public Sale Create(string productCode, int pricePerProduct, int quantity, string campaignName)
        {
            ProductCode = productCode;
            Quantity = quantity;
            TotalPaid = pricePerProduct * quantity;
            CampaignName = campaignName;
            DomainEventRepository.Add(new SaleConfirmedEvent(this));
            return this;
        }
    }

}
