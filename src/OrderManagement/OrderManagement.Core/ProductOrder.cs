using CampaignManagement.Core;
using OrderManagement.Core.Events;
using ProductManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;

namespace OrderManagement.Core
{
    public class ProductOrder : IAggregateRoot
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
        public int PricePerItemWithDiscount { get; set; }
        public int PricePerItem { get; set; }
        public int DiscountAmount { get; set; }
        public int TotalAmount { get; set; }
        public int TotalAmountWithDiscount { get; set; }
        public int TotalDiscount { get; set; }
        public int StockAfterSale { get; set; }
        public int OriginalStock { get; set; }

        public string CampaignName { get; set; }

        public Product OrderedProduct { get; set; }

        public ProductOrder Create(Product product, Order order, Campaign campaign)
        {
            ProductCode = product.ProductCode;
            Quantity = order.Quantity;
            if (campaign != null)
                DiscountAmount = product.OriginalPrice * campaign.CurrentPriceManipulation / 100;
            PricePerItem = product.OriginalPrice;
            PricePerItemWithDiscount = PricePerItem - DiscountAmount;
            TotalAmount = product.OriginalPrice * order.Quantity;
            TotalAmountWithDiscount = order.Quantity * PricePerItemWithDiscount;
            TotalDiscount = DiscountAmount * Quantity;
            OriginalStock = product.Stock;
            StockAfterSale = product.Stock - order.Quantity;

            OrderedProduct = product;
            OrderedProduct.Stock = StockAfterSale;

            CampaignName = campaign?.Name;
            DomainEventRepository.Add(new NewProductOrderCreatedEvent(this));
            return this;
        }
    }
}
