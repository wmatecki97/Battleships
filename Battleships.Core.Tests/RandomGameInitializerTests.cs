using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Battleships.Core.Models.Ships;
using FluentAssertions;
using Moq;

namespace Battleships.Core.Tests;

internal class RandomGameInitializerTests
{
    [Test]
    public void Init_DefaultInitialization_AddsTwoDestroyersAndBattleshipToTheBoardByDefault()
    {
        var gameMock = new Mock<IGame>();
        var ships = new List<IShip>();
        gameMock.Setup(g => g.Ships).Returns(ships);
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.Size).Returns(10);
        boardMock.Setup(b => b.GetField(It.IsAny<int>(), It.IsAny<int>())).Returns(() => new Field());
        gameMock.Setup(g => g.Board).Returns(boardMock.Object);
        var initializer = new DefaultBoard();

        initializer.Initialize(gameMock.Object);

        ships.Count(s => s is Battleship).Should().Be(1);
        ships.Count(s => s is Destroyer).Should().Be(2);
    }
}