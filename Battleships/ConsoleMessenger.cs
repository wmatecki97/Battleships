using Battleships.Interfaces;

namespace Battleships
{
    internal class ConsoleMessenger : IMessenger
    {
        public string GetInput()
        {
            return Console.ReadLine() ?? string.Empty;
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
