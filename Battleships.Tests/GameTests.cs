using Battleships.Interfaces;
using Battleships.Models;
using Battleships.Models.Ships;
using FluentAssertions;
using Moq;

namespace Battleships.Tests
{
    internal class GameTests
    {
        #region ShootTests
        [Test]
        public void Shoot_FieldWithShipHighHp_ReturnsHit()
        {
            //Arrange
            int x = 1, y = 1;
            const int shipLength = 5;

            var ship = new Ship(shipLength);
            var field = new Field
            {
                Ship = ship
            };
            ship.Fields.AddRange(Enumerable.Range(0, shipLength).Select(i => new Field()));

            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x,y)).Returns(field);
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(board: boardMock.Object, initializer:initializer);

            //Act
            var result = game.Shoot(x, y);

            //Assert
            result.Should().Be(EShootResult.Hit);
        }
        
        [Test]
        public void Shoot_FieldWithShipWithLastHp_ReturnsHitAndSunk()
        {
            //Arrange
            int x = 1, y = 1;
            const int shipLength = 1;
            var ship = new Ship(shipLength);
            var field = new Field
            {
                Ship = ship
            };
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x,y)).Returns(field);
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(board: boardMock.Object, initializer:initializer);

            //Act
            var result = game.Shoot(x, y);

            //Assert
            result.Should().Be(EShootResult.HitAndSunk);
        }

        [Test]
        public void Shoot_AlreadyHitField_ReturnsAlreadyHitStatus()
        {
            //Arrange
            int x = 1, y = 1;
            const int shipLength = 1;
            var field = new Field
            {
                Ship = new Ship(shipLength),
                IsHit = true
            };
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x, y)).Returns(field);
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(board: boardMock.Object, initializer: initializer);

            //Act
            var result = game.Shoot(x, y);

            //Assert
            result.Should().Be(EShootResult.AlreadyHit);
        }

        [Test]
        public void Shoot_FieldWithoutShip_ReturnsMiss()
        {
            //Arrange
            int x = 1, y = 1;
            var field = new Field();
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x,y)).Returns(field);
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(board: boardMock.Object, initializer: initializer);

            //Act
            var result = game.Shoot(x, y);

            //Assert
            result.Should().Be(EShootResult.Miss);
        }

        [Test]
        public void Shoot_FieldWithShip_FieldIsHit()
        {
            //Arrange
            int x = 1, y = 1;
            var field = new Field
            {
                Ship = new Ship(1)
            };
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x, y)).Returns(field);
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(board: boardMock.Object, initializer:initializer);

            //Act
            var result = game.Shoot(x, y);

            //Assert
            field.IsHit.Should().BeTrue();
        }

        [Test]
        public void Shoot_FieldWithShip_ShipFieldIsHit()
        {
            //Arrange
            int x = 1, y = 1;
            Ship ship = new Ship(1);
            var field = new Field
            {
                Ship = ship
            };
            ship.Fields.Add(field);
            var boardMock = new Mock<IBoard>();
            boardMock.Setup(b => b.GetField(x, y)).Returns(field);
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(board: boardMock.Object, initializer: initializer);

            //Act
            var result = game.Shoot(x, y);

            //Assert
            ship.Fields.Where(f => f.IsHit).Count().Should().Be(1);
        }

        #endregion

        #region IsGameWonTests
        [Test]
        public void IsGameWon_AllShipsDestroyed_ReturnsTrue()
        {
            //Arrange
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(initializer: initializer);
            var ship1 = GetShipWithAllFieldsHit();
            var ship2 = GetShipWithAllFieldsHit();

            game.Ships.Add(ship1);
            game.Ships.Add(ship2);

            //Act
            var isGameWon = game.IsGameWon();

            //Assert
            isGameWon.Should().BeTrue();

            static Ship GetShipWithAllFieldsHit()
            {
                var ship = new Ship(1);
                ship.Fields.Add(
                 new Field()
                 {
                     IsHit = true
                 }
                );

                return ship;
            }
        }

        [Test]
        public void IsGameWon_NoneShipsDestroyed_ReturnsFalse()
        {
            //Arrange
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(initializer: initializer);
            var ship1 = GetShipWithNoFieldsHit();
            var ship2 = GetShipWithNoFieldsHit();

            game.Ships.Add(ship1);
            game.Ships.Add(ship2);

            //Act
            var isGameWon = game.IsGameWon();
            
            //Assert
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
            //Arrange
            var initializer = new Mock<IGameInitializer>().Object;
            var game = new Game(initializer: initializer);
            var ship1 = GetShipWithNoFieldsHit();
            var ship2 = GetShipWithNoFieldsHit();

            game.Ships.Add(ship1);
            game.Ships.Add(ship2);

            //Act
            var isGameWon = game.IsGameWon();
            
            //Assert
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
