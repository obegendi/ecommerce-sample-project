using System.Linq;
using ApplicationServices.Demands;
using CampaignManagement.Core;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using EcommerceSample.TimeSimulator;
using SharedKernel;
using SharedKernel.Contracts;

namespace ApplicationServices.Time
{
    public class TimeServices : ITimeServices
    {
        private readonly IDemandServices _demandServices;
        private readonly IRepository<Campaign> _campaignRepository;
        private readonly IConsoleWriter _consoleWriter;
        private readonly SystemTimer _time;
        private readonly IUnitOfWork _unitOfWork;
        public TimeServices(IDemandServices demandServices, IRepository<Campaign> campaignRepository, IUnitOfWork unitOfWork, SystemTimer time, IConsoleWriter consoleWriter)
        {
            _demandServices = demandServices;
            _campaignRepository = campaignRepository;
            _unitOfWork = unitOfWork;
            _time = time;
            _consoleWriter = consoleWriter;
        }

        public void IncreaseTime(int hour)
        {
            var time = _time.IncreaseTime(hour);
            var campaignsToDeactive = _campaignRepository.GetAll(x => x.Status == Status.Active && x.Duration <= time.Hour);
            if (campaignsToDeactive.Any())
                foreach (var campaign in campaignsToDeactive)
                {
                    var campaignToUpdate = campaign.ChangeStatus(Status.Passive);
                    _campaignRepository.Update(campaignToUpdate);
                    _unitOfWork.Commit();
                }
            _demandServices.SimulateUpdateCampaign();
            //_demandServices.SimulatePlaceOrder();
            _consoleWriter.Write($"Time is {time.GetTime().TimeInFormat}");
        }
    }

}

