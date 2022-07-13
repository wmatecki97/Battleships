using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IBoard
    {
        Field GetField(int x, int y);
    }
}