using SharedKernel;

namespace CampaignManagement.Core.Events
{
    public class CampaignDeactivedEvent : DomainEvent
    {
        public Campaign Campaign { get; set; }
        public CampaignDeactivedEvent(Campaign campaign)
        {
            this.Campaign = campaign;
        }
    }
}