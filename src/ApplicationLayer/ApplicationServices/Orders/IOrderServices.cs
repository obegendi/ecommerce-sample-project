namespace ApplicationServices.Orders
{
    public interface IOrderServices
    {
        void CreateOrder(string productCode, int quantity);
    }
}
