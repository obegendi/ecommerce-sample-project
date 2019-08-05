using System;
using System.Linq.Expressions;
using ApplicationServices.Orders;
using CampaignManagement.Core;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using Moq;
using OrderManagement.Core;
using OrderManagement.Core.Events;
using ProductManagement.Core;
using SharedKernel;
using Xunit;

namespace EcommerceSample.Tests
{
    public class OrderServiceShould
    {
        [Theory]
        [MemberData(nameof(ProductTestData.GetProductOrderData), MemberType = typeof(ProductTestData))]
        public void AddNewProductOrderCreatedEvent(Product product, Order order, Campaign campaign)
        {
            //Arrange
            var campaignMock = new Mock<IRepository<Campaign>>();
            var productMock = new Mock<IRepository<Product>>();
            var orderMock = new Mock<IRepository<Order>>();
            var uowMock = new Mock<IUnitOfWork>();
            productMock.Setup(x => x.Get(It.IsAny<Expression<Func<Product, bool>>>()))
                .Returns(product);
            orderMock.Setup(x => x.Get(It.IsAny<Expression<Func<Order, bool>>>()))
                .Returns(order);

            var orderService = new OrderServices(orderMock.Object, productMock.Object, campaignMock.Object, uowMock.Object);

            //Act
            orderService.CreateOrder(product.ProductCode, order.Quantity);
            var events = DomainEventRepository.FindAll();

            //Assert
            Assert.Contains(events, x => x.Type == typeof(NewProductOrderCreatedEvent));
        }
    }
}
