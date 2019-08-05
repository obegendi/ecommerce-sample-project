using System;
using System.Collections.Generic;

namespace SharedKernel.Contracts
{
    public interface ISubscriptionManager
    {
        bool IsEmpty { get; }
        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH>()
            where T : DomainEvent
            where TH : IHandler<T>;

        void RemoveSubscription<T, TH>()
            where TH : IHandler<T>
            where T : DomainEvent;

        bool HasSubscriptionsForEvent(string eventName);
        Type GetEventTypeByName(string eventName);
        void Clear();
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);
        string GetEventKey<T>();
    }
}
