namespace Battleships.Core.Interfaces;

public interface IMessenger
{
    string GetInput();

    void Write(string message);
}