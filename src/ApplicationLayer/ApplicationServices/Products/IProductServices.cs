namespace ApplicationServices.Products
{
    public interface IProductServices
    {
        void AddNewProduct(string productCode, int price, int stock);
        void GetProductInfo(string productCode);
    }
}
