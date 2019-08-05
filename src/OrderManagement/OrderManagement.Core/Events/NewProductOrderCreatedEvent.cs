using SharedKernel;

namespace OrderManagement.Core.Events
{
    public class NewProductOrderCreatedEvent : DomainEvent
    {
        public NewProductOrderCreatedEvent(ProductOrder productOrder)
        {
            ProductOrder = productOrder;
        }
        public ProductOrder ProductOrder { get; }
    }
}
