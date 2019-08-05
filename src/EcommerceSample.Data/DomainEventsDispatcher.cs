using System;
using EcommerceSample.Data.Contracts;
using SharedKernel;
using SharedKernel.Contracts;

namespace EcommerceSample.Data
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly SubscriptionManager _subscriptionManager;
        public DomainEventsDispatcher(SubscriptionManager subscriptionManager)
        {
            _subscriptionManager = subscriptionManager;
        }

        /// <inheritdoc />
        public void DispatchEvents()
        {
            var events = DomainEventRepository.FindAll();
            foreach (var domainEventRecord in events)
            {
                var subscriptions = _subscriptionManager.GetHandlersForEvent(domainEventRecord.Type.Name);

                foreach (var subscription in subscriptions)
                {
                    var handler = Singleton.Container.GetInstance(subscription.HandlerType);
                    if (handler == null) continue;
                    var eventType = _subscriptionManager.GetEventTypeByName(domainEventRecord.Type.Name);
                    var concreteType = typeof(IHandler<>).MakeGenericType(eventType);
                    concreteType.GetMethod("Handle").Invoke(handler, new[] { domainEventRecord.Event });
                }

                if (!string.IsNullOrEmpty(domainEventRecord.Message))
                {
                    Console.WriteLine(domainEventRecord.Message);
                }
            }
            DomainEventRepository.ClearEvents();
        }
    }
}
