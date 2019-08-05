using ApplicationServices.Demands;
using ApplicationServices.Time;
using CampaignManagement.Core;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using EcommerceSample.TimeSimulator;
using Moq;
using SharedKernel;
using SharedKernel.Contracts;
using Xunit;

namespace EcommerceSample.Tests
{
    public class TimerServiceShould
    {
        [Fact]
        public void IncreaseTimerWhenIncrease()
        {
            var mock = new Mock<IRepository<Campaign>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            var demandMock = new Mock<IDemandServices>();
            var time = new SystemTimer();
            var timeService = new TimeServices(demandMock.Object, mock.Object, uowMock.Object, time, consoleMock.Object);

            timeService.IncreaseTime(1);

            Assert.True(time.Hour == 1);
        }

        [Fact]
        public void ReturnDailyHourWhenIncrease()
        {
            var mock = new Mock<IRepository<Campaign>>();
            var uowMock = new Mock<IUnitOfWork>();
            var consoleMock = new Mock<IConsoleWriter>();
            var demandMock = new Mock<IDemandServices>();
            var time = new SystemTimer();
            var timeService = new TimeServices(demandMock.Object, mock.Object, uowMock.Object, time, consoleMock.Object);

            timeService.IncreaseTime(30);

            Assert.True(time.Hour == 6);
        }
    }
}
