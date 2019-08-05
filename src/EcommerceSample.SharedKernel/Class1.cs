using System.Collections.Generic;

namespace EcommerceSample.SharedKernel
{
    public interface IDomainEvent
    {
    }

    public abstract class ValueObject<T> where T : ValueObject<T>
    {

    }

    public abstract class AggregateRoot : Entity
    {
        private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();
        private IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

        protected virtual void AddDomainEvent(IDomainEvent newEvent)
        {
            _domainEvents.Add(newEvent);
        }

        public virtual void ClearEvents()
        {
            _domainEvents.Clear();
        }
    }

    public interface IHandler<T>
        where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }

    public class Entity
    {
        public virtual long Id { get; protected set; }
    }

    public abstract class Repository<T>
        where T : AggregateRoot
    {
        public T GetById(long id)
        {
            return null;
        }

        public void Save(T aggregateRoot)
        {
            
        }
    }
}
