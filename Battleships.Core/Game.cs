using System.Collections.Generic;
using System.Linq;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;

namespace Battleships.Core;

public sealed class Game : IGame
{
    public Game(IBoard board)
    {
        Board = board;
    }

    public IBoard Board { get; }

    public EShootResult Shoot(int x, int y)
    {
        var field = Board.GetField(x, y);

        if (field.IsHit)
        {
            return EShootResult.AlreadyHit;
        }

        field.IsHit = true;

        if (field.Ship is not null)
        {
            return field.Ship.Fields.All(f => f.IsHit) ? EShootResult.HitAndSunk : EShootResult.Hit;
        }

        return EShootResult.Miss;
    }

    public bool IsWon()
    {
        return Board.Ships.All(s => s.Fields.All(f => f.IsHit));
    }
}