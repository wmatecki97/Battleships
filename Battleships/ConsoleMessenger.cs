using Battleships.Core.Interfaces;

namespace Battleships.Console;

internal class ConsoleMessenger : IMessenger
{
    public string GetInput()
    {
        return System.Console.ReadLine() ?? string.Empty;
    }

    public void Write(string message)
    {
        System.Console.WriteLine(message);
    }
}