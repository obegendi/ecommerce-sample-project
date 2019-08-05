namespace SharedKernel
{
    public abstract class DomainEvent
    {
        public virtual string Message { get; set; }
    }
}
