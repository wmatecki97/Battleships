using Battleships.Interfaces;
using Battleships.Models;
using FluentAssertions;
using Moq;

namespace Battleships.Tests
{
    internal class GameTests
    {
        [Test]
        public void Shoot_FieldWithShipNotSunk_ReturnsHit()
        {
            int x = 1, y = 1;
            const int shipLength = 5;
            var ship = new Ship(shipLength);
            var field = new Field
            {
                Ship = ship
            };
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x,y)).Returns(field);
            var game = new Game("player", "computer", 10, boardMock.Object);
            
            var result = game.Shoot(x, y);

            result.Should().Be(EShootResult.Hit);
        }
        
        [Test]
        public void Shoot_FieldWithShipSunk_ReturnsHitAndSunk()
        {
            int x = 1, y = 1;
            const int shipLength = 1;
            var ship = new Ship(shipLength);
            var field = new Field
            {
                Ship = ship
            };
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x,y)).Returns(field);
            var game = new Game("player", "computer", 10, boardMock.Object);

            var result = game.Shoot(x, y);

            result.Should().Be(EShootResult.HitAndSunk);
        }

        [Test]
        public void Shoot_FieldWithoutShip_ReturnsMiss()
        {
            int x = 1, y = 1;
            const int shipLength = 1;
            var field = new Field();
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x,y)).Returns(field);
            var game = new Game("player", "computer", 10, boardMock.Object);

            var result = game.Shoot(x, y);

            result.Should().Be(EShootResult.Miss);
        }
    }
}
