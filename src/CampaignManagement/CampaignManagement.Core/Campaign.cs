using CampaignManagement.Core.Events;
using SharedKernel;
using SharedKernel.Contracts;

namespace CampaignManagement.Core
{
    public class Campaign : Entity, IAggregateRoot
    {
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
        public Status Status { get; set; }
        public int CurrentPriceManipulation { get; set; }

        public Campaign CreateCampaign(string name, string productCode, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            Duration = duration;
            Name = name;
            PriceManipulationLimit = priceManipulationLimit;
            ProductCode = productCode;
            TargetSalesCount = targetSalesCount;
            Status = Status.Active;
            CurrentPriceManipulation = 0;
            DomainEventRepository.Add(new AddNewCampaignEvent(Name, ProductCode, Duration, PriceManipulationLimit, TargetSalesCount));
            return this;
        }

        public Campaign UpdatePrice(int priceManipulation)
        {
            if ((PriceManipulationLimit >= CurrentPriceManipulation + priceManipulation))
            {
                CurrentPriceManipulation += priceManipulation;
            }

            DomainEventRepository.Add(new UpdateCampaignCurrentManipulationEvent(this));
            return this;
        }

        public Campaign ChangeStatus(Status status)
        {
            Status = status;
            DomainEventRepository.Add(new CampaignDeactivedEvent(this));
            return this;
        }
    }
}
