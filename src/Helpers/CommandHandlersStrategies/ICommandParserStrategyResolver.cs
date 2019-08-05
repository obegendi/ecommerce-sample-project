using CommandHandlers.Contracts;

namespace CommandHandlers
{
    public interface ICommandParserStrategyResolver
    {
        ICommandStrategy Resolver(string[] commands);
    }
}
