using System.Collections;
using System.Collections.Generic;
using ApplicationServices.Campaigns.Validation;
using CampaignManagement.Core;
using EcommerceSample.Data;
using EcommerceSample.Data.Contracts;
using OrderManagement.Core;
using SharedKernel;
using SharedKernel.Contracts;
using ValidationModule;

namespace ApplicationServices.Campaigns
{
    public class CampaignServices : ICampaignServices
    {
        private readonly IRepository<Campaign> _campaignRepository;
        private readonly IConsoleWriter _consoleWriter;
        private readonly IRepository<Sale> _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CampaignServices(IRepository<Campaign> campaignRepository, IRepository<Sale> saleRepository, IUnitOfWork unitOfWork,
            IConsoleWriter consoleWriter)
        {
            _campaignRepository = campaignRepository;
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _consoleWriter = consoleWriter;
        }

        /// <inheritdoc />
        public void GetCampaign(string campaignName)
        {
            var entity = _campaignRepository.Get(x => x.Name == campaignName);
            var context = new ValidateContext(new ValidateCampaign(null));
            context.DoValidation(entity);
            var result = context.HandleResults();
            if (!result)
                return;

            var sales = _saleRepository.GetAll(x => x.ProductCode == entity.ProductCode);
            var productSales = new ProductSales().CreateSaleInfo(sales);
            string averagePrice = productSales.CampaignAveragePrice == 0 ? "-" : productSales.CampaignAveragePrice.ToString();
            _consoleWriter.Write(
                $"Campaign {entity.Name} info; Status {entity.Status.ToString()}, Target Sales {entity.TargetSalesCount}, Total Sales {productSales.TotalCampaignQuantity}, Turnover {productSales.TotalCampaignTurnover}, Average Item Price {averagePrice}");
        }

        public void ChangePriceManipulation(string campaignName, int priceManipulation)
        {
            var entity = _campaignRepository.Get(x => x.Name == campaignName);
            var context = new ValidateContext(new ValidateCampaign(null));
            context.DoValidation(entity);
            var result = context.HandleResults();
            if (!result)
                return;

            entity = entity.UpdatePrice(priceManipulation);
            _campaignRepository.Update(entity);
            _unitOfWork.Commit();
        }

        public IEnumerable<Campaign> GetCampaigns()
        {
            var activeCampaigns = _campaignRepository.GetAll(x => x.Status == Status.Active);
            return activeCampaigns;
        }

        /// <inheritdoc />
        public void CreateCampaign(string campaignName, string productCode, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            var newCampaign = new Campaign();
            var campaign = newCampaign.CreateCampaign(campaignName, productCode, duration, priceManipulationLimit, targetSalesCount);
            _campaignRepository.Add(campaign);
            _unitOfWork.Commit();
        }

        public void DeactiveCampaign(string campaignName)
        {
            var campaign = _campaignRepository.Get(x => x.Name == campaignName);
            campaign = campaign.ChangeStatus(Status.Passive);
            _campaignRepository.Update(campaign);
            _unitOfWork.Commit();
        }
    }
}
