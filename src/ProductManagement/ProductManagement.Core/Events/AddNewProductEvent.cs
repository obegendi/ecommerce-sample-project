using SharedKernel;

namespace ProductManagement.Core.Events
{
    public class AddNewProductEvent : DomainEvent
    {

        public AddNewProductEvent(string productCode, int price, int stock)
        {
            ProductCode = productCode;
            Price = price;
            Stock = stock;
            Message = $"Product created; code {productCode}, price {price}, stock {stock}";
        }
        public string ProductCode { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}
