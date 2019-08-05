using ProductManagement.Core;
using ValidationModule;

namespace ApplicationServices.Orders.Validation
{
    public class ValidateProductAndStock : Validate
    {
        public ValidateProductAndStock(ValidationResult previosResult) : base(previosResult)
        {
        }

        public override ValidationResult DoValidation(params object[] parameters)
        {
            var product = parameters[0] as Product;
            if (product is null)
            {
                Add(false, new ValidationResultItem("Product not found!", true));
                return Result;
            }
            if (product.Stock == 0)
                Add(false, new ValidationResultItem("Product stock 0", true));
            if (product.Stock < (int)parameters[1])
                Add(false, new ValidationResultItem("Product stock not enough!", true));
            return Result;
        }
    }
}
