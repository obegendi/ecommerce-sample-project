using System;
using ApplicationServices.Campaigns;
using CommandHandlers.Contracts;

namespace CommandHandlers.CommandStrategies
{
    public class CreateCampaignCommandStrategy : ICommandStrategy
    {
        private readonly ICampaignServices _campaignServices;
        private string _campaignName;
        private int _duration;
        private int _limit;
        private string _productCode = string.Empty;
        private int _targetSalesCount;

        public CreateCampaignCommandStrategy(ICampaignServices campaignServices)
        {
            _campaignServices = campaignServices;
        }

        /// <inheritdoc />
        public string Command { get; set; } = "create_campaign";

        /// <inheritdoc />
        public void Execute(string[] commands)
        {
            if (commands.Length != 6)
            {
                Console.WriteLine("Command parameters not valid!");
                return;
            }
            _campaignName = commands[1];
            _productCode = commands[2];
            int.TryParse(commands[3], out _duration);
            int.TryParse(commands[4], out _limit);
            int.TryParse(commands[5], out _targetSalesCount);
            _campaignServices.CreateCampaign(_campaignName, _productCode, _duration, _limit, _targetSalesCount);
        }
    }

}
