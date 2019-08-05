using System.Collections;
using System.Collections.Generic;
using CampaignManagement.Core;

namespace ApplicationServices.Campaigns
{
    public interface ICampaignServices
    {
        void GetCampaign(string campaignName);
        void CreateCampaign(string campaignName, string productCode, int duration, int priceManipulationLimit, int targetSalesCount);
        void ChangePriceManipulation(string campaignName, int priceManipulation);
        IEnumerable<Campaign> GetCampaigns();
    }
}
