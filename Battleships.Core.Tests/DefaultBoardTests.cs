using Battleships.Core.Models.Boards;
using Battleships.Core.Models.Ships;
using FluentAssertions;

namespace Battleships.Core.Tests;

internal class DefaultBoardTests
{
    [Test]
    public void Init_DefaultInitialization_AddsTwoDestroyersAndBattleshipToTheBoardByDefault()
    {
        var board = new DefaultBoard();

        board.Ships.Where(s => s is Destroyer).Should().HaveCount(2);
        board.Ships.Where(s => s is Battleship).Should().HaveCount(1);
        board.Ships.Should().HaveCount(3);
    }
}