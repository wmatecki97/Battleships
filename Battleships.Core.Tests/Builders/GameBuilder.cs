using Battleships.Core.Interfaces;
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

    public Game Build()
    {
        _boardMock.Setup(board => board.Ships).Returns(_ships);
        return new Game(_boardMock.Object);
    }
}