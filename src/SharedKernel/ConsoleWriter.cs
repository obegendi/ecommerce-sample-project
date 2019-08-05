using System;
using SharedKernel.Contracts;

namespace SharedKernel
{
    public class ConsoleWriter : IConsoleWriter
    {
        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }

}
