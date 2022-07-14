using Battleships.Interfaces;

namespace Battleships
{
    internal class ConsoleMessenger : IMessenger
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
