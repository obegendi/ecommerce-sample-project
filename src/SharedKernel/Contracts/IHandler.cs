using System;

namespace SharedKernel.Contracts
{
    public interface IHandler<T>
        where T : DomainEvent
    {
        Type Type { get; }
        void Handle(T domainEvent);
    }
}
