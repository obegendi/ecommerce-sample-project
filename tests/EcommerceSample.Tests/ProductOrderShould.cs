using CampaignManagement.Core;
using OrderManagement.Core;
using ProductManagement.Core;
using Xunit;

namespace EcommerceSample.Tests
{
    public class ProductOrderShould
    {
        [Theory]
        [MemberData(nameof(ProductTestData.GetProductOrderData), MemberType = typeof(ProductTestData))]
        public void ReturnTotalAmount(Product product, Order order, Campaign campaign)
        {
            var productOrder = new ProductOrder();

            productOrder = productOrder.Create(product, order, campaign);
            var totalAmount = product.OriginalPrice * order.Quantity;

            //Assert
            Assert.True(productOrder.TotalAmount == totalAmount);
        }

        [Theory]
        [MemberData(nameof(ProductTestData.GetProductOrderData), MemberType = typeof(ProductTestData))]
        public void ReturnOrderWithDiscountPrice(Product product, Order order, Campaign campaign)
        {
            var productOrder = new ProductOrder();

            productOrder = productOrder.Create(product, order, campaign);
            var price = product.OriginalPrice - product.OriginalPrice * campaign.PriceManipulationLimit / 100;
            //Assert
            Assert.True(productOrder.PricePerItemWithDiscount == price);
        }
    }
}
