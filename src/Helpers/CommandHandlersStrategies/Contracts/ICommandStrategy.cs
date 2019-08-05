namespace CommandHandlers.Contracts
{
    public interface ICommandStrategy
    {
        string Command { get; set; }
        void Execute(string[] commands);
    }
}
