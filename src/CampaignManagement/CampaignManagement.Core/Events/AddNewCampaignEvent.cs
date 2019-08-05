using SharedKernel;

namespace CampaignManagement.Core.Events
{
    public class AddNewCampaignEvent : DomainEvent
    {

        public AddNewCampaignEvent(string name, string productCode, int duration, int priceManipulationLimit, int targetSalesCount)
        {
            Name = name;
            ProductCode = productCode;
            Duration = duration;
            PriceManipulationLimit = priceManipulationLimit;
            TargetSalesCount = targetSalesCount;
            base.Message =
                $"Campaign created; name {name}, product {productCode}, duration {duration},limit {priceManipulationLimit}, target sales count {targetSalesCount}";
        }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public int Duration { get; set; }
        public int PriceManipulationLimit { get; set; }
        public int TargetSalesCount { get; set; }
    }
}
