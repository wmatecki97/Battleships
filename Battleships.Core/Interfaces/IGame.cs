using System.Collections.Generic;
using Battleships.Core.Models;
using Battleships.Core.Models.Ships;

namespace Battleships.Core.Interfaces;

public interface IGame
{
    List<Ship> Ships { get; }

    IBoard Board { get; }

    EShootResult Shoot(int x, int y);

    bool IsGameWon();
}