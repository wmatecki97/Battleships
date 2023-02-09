using FluentAssertions;

namespace Battleships.Tests;

internal class GameInitializerTests
{
    [Test]
    public void Init_RandomShipPlacement_AllShipsHaveCorrectNoOfFieldsAssigned()
    {
        var game = new Game();

        game.Ships.ForEach(s => s.Fields.Count.Should().Be(s.Length));
        int noOfFieldsWithShips = game.Board.Fields.Count(f => f.Ship is not null);
        int totalShipsLength = game.Ships.Sum(s => s.Length);
        noOfFieldsWithShips.Should().Be(totalShipsLength);
    }
}