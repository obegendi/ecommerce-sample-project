using SharedKernel;

namespace OrderManagement.Core.Events
{
    public class NewOrderCreatedEvent : DomainEvent
    {
        public NewOrderCreatedEvent(string productCode, int quantity)
        {
            ProductCode = productCode;
            Quantity = quantity;
            Message = $"Order created; product {productCode}, quantity {quantity}";

        }
        public string ProductCode { get; set; }

        public int Quantity { get; set; }
    }
}
