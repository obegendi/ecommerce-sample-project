using System;
using SharedKernel.Contracts;

namespace SharedKernel
{
    public class DomainEventsHandlerDispatcher<T> where T : DomainEvent
    {

        public void DispatchHandler(T domainEvent)
        {
            Activator.CreateInstance<IHandler<T>>().Handle(domainEvent);
        }
    }
}
