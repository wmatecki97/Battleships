using Battleships.Core.Exceptions;
using Battleships.Core.Interfaces;
using Battleships.Core.Models.Boards;
using Battleships.Core.Models.Ships;
using FluentAssertions;
using Moq;

namespace Battleships.Core.Tests.Models;

internal class RandomShipPlacementBoardBaseTests
{
    [Test]
    public void RandomShipPlacementBoardCreation_MultipleShipsGiven_AllShipsHaveCorrectNoOfFieldsAssigned()
    {
        //Arrange
        var ships = new IShip[]
        {
            new Destroyer(),
            new Battleship()
        };

        //Act
        var board = new RandomShipPlacementBoard(ships, 10, new RandomNumberGenerator());

        //Assert
        board.Ships.All(s => s.Fields.Count == s.Length).Should().BeTrue();
    }

    [Test]
    public void RandomShipPlacementBoardCreation_MultipleShipsGiven_FieldsWithShipsCountIsSameAsTotalShipsLength()
    {
        //Arrange
        var ships = new IShip[]
        {
            new Destroyer(),
            new Battleship()
        };
        var expectedFieldsWithShipsCount = ships.Sum(s => s.Length);

        //Act
        var board = new RandomShipPlacementBoard(ships, 10, new RandomNumberGenerator());

        board.Fields.Where(f => f.Ship != null).Should().HaveCount(expectedFieldsWithShipsCount);
    }

    [TestCase(4, 8)]//4x4 board with 4 length ship gives 4 horizontal + 4 vertical possible placements
    [TestCase(10, 140)]
    public void RandomShipPlacementBoardCreation_OneDestroyerOnly_UsesAllPossiblePlacementsToPutTheShipOnBoard(int boardSize, int numberOfPossiblePlacements)
    {
        //Arrange
        var ships = new IShip[]
        {
            new Destroyer(),
        };

        var randomNumberGeneratorMock = new Mock<IRandomNumberGenerator>();
        randomNumberGeneratorMock.Setup(x => x.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
            .Returns((int start, int _) => start);

        //Act
        new RandomShipPlacementBoard(ships, boardSize, randomNumberGeneratorMock.Object);

        //Assert
        randomNumberGeneratorMock.Verify(x => x.GetRandomNumber(0, numberOfPossiblePlacements), Times.Once);
    }

    [Test]
    public void RandomShipPlacementBoardCreation_TooManyShipsOnBoard_ThrowsNotEnoughPlaceOnTheBoardException()
    {
        //Arrange
        var ships = new IShip[]
        {
            new Destroyer(),
        };

        const int lessThanDestroyerLength = 3;

        //Act
        Action boardCreation =() => new RandomShipPlacementBoard(ships, lessThanDestroyerLength, new RandomNumberGenerator());

        //Asssert
        boardCreation.Should().ThrowExactly<NotEnoughPlaceOnTheBoardException>();
    }
}