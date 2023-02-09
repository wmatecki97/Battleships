using Battleships.Interfaces;
using Battleships.Models;
using Battleships.Models.Ships;
using FluentAssertions;
using Moq;

namespace Battleships.Tests;

internal class RandomGameInitializerTests
{
    [Test]
    public void Init_DefaultInitialization_AddsTwoDestroyersAndBattleshipToTheBoardByDefault()
    {
        var gameMock = new Mock<IGame>();
        var ships = new List<Ship>();
        gameMock.Setup(g => g.Ships).Returns(ships);
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.Size).Returns(10);
        boardMock.Setup(b => b.GetField(It.IsAny<int>(), It.IsAny<int>())).Returns(() => new Field());
        gameMock.Setup(g => g.Board).Returns(boardMock.Object);
        var initializer = new RandomGameInitializer();

        initializer.Initialize(gameMock.Object);

        ships.Count(s => s is Battleship).Should().Be(1);
        ships.Count(s => s is Destroyer).Should().Be(2);
    }
}