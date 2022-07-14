using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IBoard
    {
        int Size { get; }

        Field[] Fields { get; }

        Field GetField(int x, int y);
    }
}