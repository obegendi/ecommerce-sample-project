using System;
using ApplicationServices.Campaigns;
using CommandHandlers.Contracts;

namespace CommandHandlers.CommandStrategies
{
    public class GetCampaignInfoCommandStrategy : ICommandStrategy
    {
        private readonly ICampaignServices _campaignServices;
        private string _campaignName;

        public GetCampaignInfoCommandStrategy(ICampaignServices campaignServices)
        {
            _campaignServices = campaignServices;
        }

        /// <inheritdoc />
        public string Command { get; set; } = "get_campaign_info";

        /// <inheritdoc />
        public void Execute(string[] commands)
        {
            if (commands.Length != 2)
            {
                Console.WriteLine("Command parameters not valid!");
                return;
            }
            _campaignName = commands[1];
            _campaignServices.GetCampaign(_campaignName);
        }
    }
}
