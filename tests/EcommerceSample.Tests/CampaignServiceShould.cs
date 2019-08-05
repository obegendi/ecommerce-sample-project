using System;
using System.Linq;
using System.Linq.Expressions;
using ApplicationServices.Campaigns;
using CampaignManagement.Core;
using CampaignManagement.Core.Events;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using Moq;
using OrderManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;
using Xunit;

namespace EcommerceSample.Tests
{
    public class CampaignServiceShould
    {

        [Fact]
        public void AddCreateCampaignEvent()
        {
            //Arrange
            var mock = new Mock<IRepository<Campaign>>();
            var saleMock = new Mock<IRepository<Sale>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            mock.Setup(x => x.Add(It.IsAny<Campaign>()))
                .Returns(new Campaign { ProductCode = "P1", Name = "C1", Status = Status.Active, Duration = 5 }).Verifiable();
            var campaignService = new CampaignServices(mock.Object, saleMock.Object, uowMock.Object, consoleMock.Object);

            //Act
            campaignService.CreateCampaign("C1", "P1", 10, 20, 100);
            var events = DomainEventRepository.FindAll();

            //Assert
            Assert.Contains(events, x => x.Type == typeof(AddNewCampaignEvent));
        }

        [Fact]
        public void AddCreateCampaignEventContainsCampaignName()
        {
            //Arrange
            var mock = new Mock<IRepository<Campaign>>();
            var saleMock = new Mock<IRepository<Sale>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            mock.Setup(x => x.Add(It.IsAny<Campaign>()))
                .Returns(new Campaign { ProductCode = "P1", Name = "C1", Status = Status.Active, Duration = 5 }).Verifiable();
            var campaignService = new CampaignServices(mock.Object, saleMock.Object, uowMock.Object, consoleMock.Object);

            //Act
            campaignService.CreateCampaign("C1", "P1", 10, 20, 100);
            var events = DomainEventRepository.FindAll();
            var domainEvent = events.FirstOrDefault(x => x.Type == typeof(AddNewCampaignEvent)).Event as AddNewCampaignEvent;

            //Assert
            Assert.True(domainEvent.Name == "C1");
        }

        [Fact]
        public void AddCreateCampaignEventContainsProductCode()
        {
            //Arrange
            var mock = new Mock<IRepository<Campaign>>();
            var saleMock = new Mock<IRepository<Sale>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            mock.Setup(x => x.Add(It.IsAny<Campaign>()))
                .Returns(new Campaign { ProductCode = "P1", Name = "C1", Status = Status.Active, Duration = 5 }).Verifiable();
            var campaignService = new CampaignServices(mock.Object, saleMock.Object, uowMock.Object, consoleMock.Object);

            //Act
            campaignService.CreateCampaign("C1", "P1", 10, 20, 100);
            var events = DomainEventRepository.FindAll();
            var domainEvent = events.FirstOrDefault(x => x.Type == typeof(AddNewCampaignEvent)).Event as AddNewCampaignEvent;

            //Assert
            Assert.True(domainEvent.ProductCode == "P1");
        }

        [Fact]
        public void AddCreateCampaignEventShouldNotEmptyMessage()
        {
            //Arrange
            var mock = new Mock<IRepository<Campaign>>();
            var saleMock = new Mock<IRepository<Sale>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            mock.Setup(x => x.Add(It.IsAny<Campaign>()))
                .Returns(new Campaign { ProductCode = "P1", Name = "C1", Status = Status.Active, Duration = 5 }).Verifiable();
            var campaignService = new CampaignServices(mock.Object, saleMock.Object, uowMock.Object, consoleMock.Object);

            //Act
            campaignService.CreateCampaign("C1", "P1", 10, 20, 100);
            var events = DomainEventRepository.FindAll();
            var domainEvent = events.FirstOrDefault(x => x.Type == typeof(AddNewCampaignEvent)).Event as AddNewCampaignEvent;

            //Assert
            Assert.False(string.IsNullOrEmpty(domainEvent.Message));
        }

        [Fact]
        public void CreateCampaign()
        {
            //Arrange
            var mock = new Mock<IRepository<Campaign>>();
            var saleMock = new Mock<IRepository<Sale>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            mock.Setup(x => x.Add(It.IsAny<Campaign>()))
                .Returns(new Campaign { ProductCode = "P1", Name = "C1", Status = Status.Active, Duration = 5 }).Verifiable();
            var campaignService = new CampaignServices(mock.Object, saleMock.Object, uowMock.Object, consoleMock.Object);

            //Act
            campaignService.CreateCampaign("C1", "P1", 10, 20, 100);

            //Assert
            uowMock.Verify(x => x.Commit(), Times.Once);
        }
        [Fact]
        public void WriteCampaignToWriter()
        {
            //Arrange
            var mock = new Mock<IRepository<Campaign>>();
            var saleMock = new Mock<IRepository<Sale>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            mock.Setup(x => x.Get(It.IsAny<Expression<Func<Campaign, bool>>>()))
                .Returns(new Campaign { ProductCode = "P1", Name = "C1", Status = Status.Active, Duration = 5 }).Verifiable();
            var campaignService = new CampaignServices(mock.Object, saleMock.Object, uowMock.Object, consoleMock.Object);

            //Act
            campaignService.GetCampaign("C1");

            //Assert
            consoleMock.Verify(x => x.Write(It.IsAny<string>()), Times.Once);
        }
    }

}
