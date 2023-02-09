using System.Collections.Generic;
using System.Linq;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Battleships.Core.Models.Ships;

namespace Battleships.Core;

public sealed class Game : IGame
{
    public Game(int boardSize = 10, IBoard? board = null, IGameInitializer? initializer = null)
    {
        Board = board ?? new Board(boardSize);
        Ships = new List<Ship>();
        initializer ??= new RandomGameInitializer();
        initializer.Initialize(this);
    }

    public IBoard Board { get; }
    public List<Ship> Ships { get; }

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

    public bool IsGameWon()
    {
        return Ships.All(s => s.Fields.All(f => f.IsHit));
    }
}