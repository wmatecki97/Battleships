using Battleships.Interfaces;
using Battleships.Models;
using FluentAssertions;
using Moq;

namespace Battleships.Tests
{
    internal class GameTests
    {
        [Test]
        public void Init_RandomShipPlacement_AllShipsHaveCorrectNoOfFieldsAssigned()
        {
            var game = new Game(10);

            game.Ships.ForEach(s => s.Fields.Count.Should().Be(s.Length));
            int noOfFieldsWithShips = game.Board.Fields.Where(f => f.Ship is not null).Count();
            int totalShipsLength = game.Ships.Sum(s => s.Length);
            noOfFieldsWithShips.Should().Be(totalShipsLength);
        }

        #region ShootTests
        [Test]
        public void Shoot_FieldWithShipHighHp_ReturnsHit()
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
            var game = new Game(10, boardMock.Object);
            
            var result = game.Shoot(x, y);

            result.Should().Be(EShootResult.Hit);
        }
        
        [Test]
        public void Shoot_FieldWithShipWithLastHp_ReturnsHitAndSunk()
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
            var game = new Game(10, boardMock.Object);

            var result = game.Shoot(x, y);

            result.Should().Be(EShootResult.HitAndSunk);
        }

        [Test]
        public void Shoot_FieldWithoutShip_ReturnsMiss()
        {
            int x = 1, y = 1;
            var field = new Field();
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x,y)).Returns(field);
            var game = new Game(10, boardMock.Object);

            var result = game.Shoot(x, y);

            result.Should().Be(EShootResult.Miss);
        }

        [Test]
        public void Shoot_FieldWithShip_FieldIsHit()
        {
            int x = 1, y = 1;
            var field = new Field
            {
                Ship = new Ship(1)
            };
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x, y)).Returns(field);
            var game = new Game(10, boardMock.Object);

            var result = game.Shoot(x, y);

            field.isHit.Should().BeTrue();
        }

        [Test]
        public void Shoot_FieldWithoutShip_FieldIsHit()
        {
            int x = 1, y = 1;
            var field = new Field();
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x, y)).Returns(field);
            var game = new Game(10, boardMock.Object);

            var result = game.Shoot(x, y);

            field.isHit.Should().BeTrue();
        }

        #endregion

        #region IsGameWonTests
        [Test]
        public void IsGameWon_AllShipsDestroyed_ReturnsTrue()
        {
            const int BoardSize = 10;
            var game = new Game(BoardSize);
            var ship1 = GetShipWithAllFieldsHit();
            var ship2 = GetShipWithAllFieldsHit();

            game.Ships.Add(ship1);
            game.Ships.Add(ship2);

            var isGameWon = game.IsGameWon();
            isGameWon.Should().BeTrue();

            static Ship GetShipWithAllFieldsHit()
            {
                var ship = new Ship(1);
                ship.Fields.Add(
                 new Field()
                 {
                     isHit = true
                 }
                );

                return ship;
            }
        }

        [Test]
        public void IsGameWon_NoneShipsDestroyed_ReturnsFalse()
        {
            const int BoardSize = 10;
            var game = new Game(BoardSize);
            var ship1 = GetShipWithNoFieldsHit();
            var ship2 = GetShipWithNoFieldsHit();

            game.Ships.Add(ship1);
            game.Ships.Add(ship2);

            var isGameWon = game.IsGameWon();
            isGameWon.Should().BeFalse();

            static Ship GetShipWithNoFieldsHit()
            {
                var ship = new Ship(1);
                ship.Fields.Add(new Field());

                return ship;
            }
        }

        [Test]
        public void IsGameWon_SomeShipsDestroyed_ReturnsFalse()
        {
            const int BoardSize = 10;
            var game = new Game(BoardSize);
            var ship1 = GetShipWithNoFieldsHit();
            var ship2 = GetShipWithNoFieldsHit();

            game.Ships.Add(ship1);
            game.Ships.Add(ship2);

            var isGameWon = game.IsGameWon();
            isGameWon.Should().BeFalse();

            static Ship GetShipWithNoFieldsHit()
            {
                var ship = new Ship(1);
                ship.Fields.Add(new Field());

                return ship;
            }
        }
        #endregion
    }
}
