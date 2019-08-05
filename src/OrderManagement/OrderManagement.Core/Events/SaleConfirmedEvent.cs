using SharedKernel;

namespace OrderManagement.Core.Events
{
    public class SaleConfirmedEvent : DomainEvent
    {
        public SaleConfirmedEvent(Sale sale)
        {

        }
    }
}
