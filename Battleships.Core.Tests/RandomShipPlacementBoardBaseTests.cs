using Battleships.Core.Interfaces;
using Battleships.Core.Models.Boards;
using Battleships.Core.Models.Ships;
using FluentAssertions;

namespace Battleships.Core.Tests;

internal class RandomShipPlacementBoardBaseTests
{
    [Test]
    public void RandomShipPlacementBoardCreation_MultipleShipsGiven_AllShipsHaveCorrectNoOfFieldsAssigned()
    {
        var ships = new IShip[]
        {
            new Destroyer(),
            new Battleship()
        };

        var board = new TestRandomShipPlacementBoard(ships, 10);

        board.Ships.ToList().ForEach(s =>
            s.Fields.Count.Should().Be(s.Length, $"Ship of length {s.Length} should have {s.Length} fields assigned."));
    }

    [Test]
    public void RandomShipPlacementBoardCreation_MultipleShipsGiven_FieldsWithShipsCountIsSameAsTotalShipsLength()
    {
        var ships = new IShip[]
        {
            new Destroyer(),
            new Battleship()
        };

        var board = new TestRandomShipPlacementBoard(ships, 10);

        int noOfFieldsWithShips = board.Fields.Count(f => f.Ship != null);
        int totalShipsLength = board.Ships.Sum(s => s.Length);
        noOfFieldsWithShips.Should().Be(totalShipsLength,
            $"Number of fields with ships should be {totalShipsLength}, but it was {noOfFieldsWithShips}");
    }

    private class TestRandomShipPlacementBoard : RandomShipPlacementBoardBase
    {
        public TestRandomShipPlacementBoard(IEnumerable<IShip> ships, int size) : base(ships, size)
        {
        }
    }
}