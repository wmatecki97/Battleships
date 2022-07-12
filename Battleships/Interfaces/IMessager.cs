namespace Battleships.Interfaces
{
    public interface IMessager
    {
        string GetInput();
        void Write(string message);
    }
}