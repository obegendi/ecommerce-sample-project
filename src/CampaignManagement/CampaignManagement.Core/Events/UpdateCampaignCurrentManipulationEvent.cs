using SharedKernel;

namespace CampaignManagement.Core.Events
{
    public class UpdateCampaignCurrentManipulationEvent : DomainEvent
    {
        public Campaign Campaign { get; set; }

        public UpdateCampaignCurrentManipulationEvent(Campaign campaign)
        {
            Campaign = campaign;
        }
    }
}