using System;
using System.Collections.Generic;

namespace SharedKernel
{
    public static class DomainEventRepository
    {
        private static readonly List<DomainEventRecord> domainEventRecords = new List<DomainEventRecord>();

        public static void Add<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent
        {
            domainEventRecords.Add(
                new DomainEventRecord
                {
                    Created = DateTime.Now,
                    Event = domainEvent,
                    Type = domainEvent.GetType(),
                    Message = domainEvent.Message
                });
        }

        public static IEnumerable<DomainEventRecord> FindAll()
        {
            return domainEventRecords;
        }

        public static void ClearEvent<TDomainEvent>(TDomainEvent domainEvent)
        {
            var @event = domainEvent as DomainEventRecord;
            var removeCount = domainEventRecords.RemoveAll(x => x.Type.Name == @event.Type.Name && x.Event == @event.Event);
        }

        public static void ClearEvents()
        {
            domainEventRecords.Clear();
        }
    }

}
