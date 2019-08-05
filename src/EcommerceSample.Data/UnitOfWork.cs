using EcommerceSample.Data.Contracts;

namespace EcommerceSample.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EcommerceContext _context;
        private readonly IDomainEventsDispatcher _dispatcher;
        public UnitOfWork(EcommerceContext context, IDomainEventsDispatcher dispatcher)
        {
            _context = context;
            _dispatcher = dispatcher;
        }

        public int Commit()
        {
            _dispatcher.DispatchEvents();
            var result = _context.SaveChanges();
            return result;
        }
    }
}
