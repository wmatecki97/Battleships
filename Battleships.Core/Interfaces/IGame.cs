using System.Collections.Generic;
using Battleships.Core.Models;

namespace Battleships.Core.Interfaces;

public interface IGame
{
    IBoard Board { get; }

    EShootResult Shoot(int x, int y);

    bool IsGameWon();
}