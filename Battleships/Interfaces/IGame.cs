using Battleships.Models;

namespace Battleships.Interfaces
{
    public interface IGame
    {
        int BoardSize { get; }
        
        EShootResult Shoot(int x, int y);

        bool IsGameWon();
    }
}