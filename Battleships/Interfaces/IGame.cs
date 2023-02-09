using Battleships.Models;
using Battleships.Models.Ships;

namespace Battleships.Interfaces;

public interface IGame
{
    List<Ship> Ships { get; }

    IBoard Board { get; }

    EShootResult Shoot(int x, int y);

    bool IsGameWon();
}