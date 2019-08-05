using ApplicationServices.Campaigns;
using ApplicationServices.Orders;
using EcommerceSample.Data.Contracts;
using ProductManagement.Core;

namespace ApplicationServices.Demands
{
    /// <summary>
    /// Demand simulation (not real application service)
    /// </summary>
    public class DemandServices : IDemandServices
    {
        private readonly ICampaignServices _campaignServices;
        private readonly IOrderServices _orderServices;
        private readonly IRepository<Product> _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        private const int Quantity = 5;
        private const int PriceManipulation = 5;

        public DemandServices(ICampaignServices campaignServices, IOrderServices orderServices, IRepository<Product> productRepository, IUnitOfWork unitOfWork)
        {
            _campaignServices = campaignServices;
            _orderServices = orderServices;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }
        public void SimulateUpdateCampaign()
        {
            var campaings = _campaignServices.GetCampaigns();
            foreach (var campaing in campaings)
            {
                if (campaing.PriceManipulationLimit > campaing.CurrentPriceManipulation)
                {
                    _campaignServices.ChangePriceManipulation(campaing.Name, PriceManipulation);
                }
            }
            _unitOfWork.Commit();
        }

        public void SimulatePlaceOrder()
        {
            var products = _productRepository.GetAll(x => x.Stock > 0);
            foreach (var product in products)
            {
                _orderServices.CreateOrder(product.ProductCode, Quantity);
            }
        }
    }
}
