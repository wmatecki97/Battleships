using Battleships.Core.Interfaces;
using Battleships.Core.Models.Boards;
using Battleships.Core.Models.Ships;
using FluentAssertions;

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
        var board = new RandomShipPlacementBoard(ships, 10);

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
        var board = new RandomShipPlacementBoard(ships, 10);

        board.Fields.Where(f => f.Ship != null).Should().HaveCount(expectedFieldsWithShipsCount);
    }
}