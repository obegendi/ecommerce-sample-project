using System;

namespace SharedKernel
{
    public class DomainEventRecord
    {
        public Type Type { get; set; }
        public object Event { get; set; }
        public DateTime Created { get; set; }
        public string Message { get; set; }
    }
}
