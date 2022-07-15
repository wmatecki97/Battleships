namespace Battleships.Interfaces
{
    public interface IMessenger
    {
        string GetInput();

        void Write(string message);
    }
}