using ProductManagement.Core.Events;
using SharedKernel;
using SharedKernel.Contracts;

namespace ProductManagement.Core
{
    public class Product : Entity, IAggregateRoot
    {
        public string ProductCode { get; set; }

        public int OriginalPrice { get; set; }

        public int Stock { get; set; }

        public int DiscountedPrice { get; set; }

        public void Create(string productCode, int price, int stock)
        {
            DiscountedPrice = price;
            DomainEventRepository.Add(new AddNewProductEvent(productCode, price, stock));
        }
    }

}
