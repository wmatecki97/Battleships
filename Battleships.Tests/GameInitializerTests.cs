using FluentAssertions;

namespace Battleships.Tests
{
    internal class GameInitializerTests
    {
        [Test]
        public void Init_RandomShipPlacement_AllShipsHaveCorrectNoOfFieldsAssigned()
        {
            var game = new Game();

            game.Ships.ForEach(s => s.Fields.Count.Should().Be(s.Length));
            int noOfFieldsWithShips = game.Board.Fields.Where(f => f.Ship is not null).Count();
            int totalShipsLength = game.Ships.Sum(s => s.Length);
            noOfFieldsWithShips.Should().Be(totalShipsLength);
        }
    }
}
