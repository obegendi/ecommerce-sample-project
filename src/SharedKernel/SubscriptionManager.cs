using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel.Contracts;

namespace SharedKernel
{
    public class SubscriptionManager : ISubscriptionManager
    {
        private readonly List<Type> _eventTypes;
        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;

        public SubscriptionManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        public event EventHandler<string> OnEventRemoved;

        public bool IsEmpty => !_handlers.Keys.Any();
        public void Clear()
        {
            _handlers.Clear();
        }

        public void AddSubscription<T, TH>()
            where T : DomainEvent
            where TH : IHandler<T>
        {
            var eventName = GetEventKey<T>();

            if (!HasSubscriptionsForEvent(eventName))
                _handlers.Add(eventName, new List<SubscriptionInfo>());

            if (_handlers[eventName].Any(s => s.HandlerType == typeof(TH)))
                return;

            _handlers[eventName].Add(SubscriptionInfo.Typed(typeof(TH)));
            if (!_eventTypes.Contains(typeof(T)))
                _eventTypes.Add(typeof(T));
        }

        public void RemoveSubscription<T, TH>()
            where TH : IHandler<T>
            where T : DomainEvent
        {

            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();

            if (handlerToRemove != null)
            {
                _handlers[eventName].Remove(handlerToRemove);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                        _eventTypes.Remove(eventType);
                    var handler = OnEventRemoved;
                    if (handler != null)
                        OnEventRemoved(this, eventName);
                }

            }
        }

        public bool HasSubscriptionsForEvent(string eventName)
        {
            return _handlers.ContainsKey(eventName);
        }

        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(t => t.Name == eventName);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            var subList = new List<SubscriptionInfo>();
            var tryGet = _handlers.TryGetValue(eventName, out subList);
            if (!tryGet) subList = new List<SubscriptionInfo>();
            return subList;
        }

        private SubscriptionInfo FindSubscriptionToRemove<T, TH>()
            where T : DomainEvent
            where TH : IHandler<T>
        {
            var eventName = GetEventKey<T>();
            if (!HasSubscriptionsForEvent(eventName))
                return null;
            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == typeof(TH));
        }
    }
}
