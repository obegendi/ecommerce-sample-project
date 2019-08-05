using System.Collections.Generic;
using CampaignManagement.Core;
using OrderManagement.Core;
using ProductManagement.Core;

namespace EcommerceSample.Tests
{
    public static class ProductTestData
    {
        public static IEnumerable<object[]> GetProductOrderData()
        {
            return new[]
            {
                new object[]
                {
                    new Product { Id = 1, OriginalPrice = 10, ProductCode = "P1", Stock = 100 },
                    new Order { Id = 1, ProductCode = "P1", Quantity = 3 },
                    new Campaign
                    {
                        ProductCode = "P1", PriceManipulationLimit = 20, Duration = 5, Id = 1, Name = "C1", TargetSalesCount = 50, Status = Status.Active
                    }
                }

            };
        }
    }
}
