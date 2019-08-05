namespace EcommerceSample.Data.Contracts
{
    public interface IUnitOfWork
    {
        int Commit();
    }
}
