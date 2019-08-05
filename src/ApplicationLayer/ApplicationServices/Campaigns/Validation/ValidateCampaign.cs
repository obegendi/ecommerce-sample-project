using CampaignManagement.Core;
using ValidationModule;

namespace ApplicationServices.Campaigns.Validation
{
    public class ValidateCampaign : Validate
    {
        public ValidateCampaign(ValidationResult previosResult) : base(previosResult)
        {
        }

        public override ValidationResult DoValidation(params object[] parameters)
        {
            var campaign = parameters[0] as Campaign;
            if (campaign is null)
            {
                Add(false, new ValidationResultItem("Campaign not found!", true));
                return Result;
            }
            if (campaign.Duration == 0)
                Add(false, new ValidationResultItem("Campaign Over!", true));

            return Result;
        }
    }
}
