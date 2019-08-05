using System.Collections.Generic;
using System.Linq;
using SharedKernel;
using SharedKernel.Contracts;

namespace OrderManagement.Core
{
    public class ProductSales : IAggregateRoot
    {
        public string ProductCode { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalPaid { get; set; }
        public decimal AveragePrice { get; set; }
        public int TotalCampaignQuantity { get; set; }
        public int TotalCampaignTurnover { get; set; }
        public int CampaignAveragePrice { get; set; }

        public ProductSales CreateSaleInfo(IEnumerable<Sale> sales)
        {
            ProductCode = sales.FirstOrDefault()?.ProductCode;
            foreach (var sale in sales)
            {
                TotalPaid += sale.TotalPaid;
                TotalQuantity += sale.Quantity;
                if (!string.IsNullOrEmpty(sale.CampaignName))
                {
                    TotalCampaignTurnover += sale.TotalPaid;
                    TotalCampaignQuantity += sale.Quantity;
                    CampaignAveragePrice = TotalCampaignTurnover / TotalCampaignQuantity;
                }
                AveragePrice = TotalPaid / TotalQuantity;
            }
            
            return this;
        }
    }
}
