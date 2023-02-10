using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Battleships.Core.Models.Ships;
using Moq;

namespace Battleships.Core.Tests.Builders;

internal class GameBuilder
{
    private readonly Mock<IBoard> _boardMock = new();
    private readonly List<IShip> _ships = new();

    public GameBuilder WithShip(IShip ship1)
    {
        _ships.Add(ship1);
        return this;
    }

    public GameBuilder WithDestroyedShip()
    {
        var ship = new Destroyer
        {
            Fields =
            {
                new Field { IsHit = true },
                new Field { IsHit = true },
                new Field { IsHit = true },
                new Field { IsHit = true }
            }
        };
        _ships.Add(ship);
        return this;
    }

    public GameBuilder WithNotDestroyedShip()
    {
        var ship = new Destroyer
        {
            Fields =
            {
                new Field(),
                new Field(),
                new Field(),
                new Field()
            }
        };
        _ships.Add(ship);
        return this;
    }

    public Game Build()
    {
        _boardMock.Setup(board => board.Ships).Returns(_ships);
        return new Game(_boardMock.Object);
    }
}