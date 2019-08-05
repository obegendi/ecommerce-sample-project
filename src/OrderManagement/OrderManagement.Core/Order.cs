using OrderManagement.Core.Events;
using SharedKernel;
using SharedKernel.Contracts;

namespace OrderManagement.Core
{
    public class Order : Entity, IAggregateRoot
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }

        public Order Create(int quantity, string productCode)
        {
            ProductCode = productCode;
            Quantity = quantity;
            DomainEventRepository.Add(new NewOrderCreatedEvent(productCode, quantity));
            return this;
        }
    }

}
