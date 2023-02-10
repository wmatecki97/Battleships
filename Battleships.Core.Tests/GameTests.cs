using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Battleships.Core.Models.Ships;
using Battleships.Core.Tests.Builders;
using FluentAssertions;
using Moq;

namespace Battleships.Core.Tests;

internal class GameTests
{
    #region ShootTests

    //todo builder
    [Test]
    public void Shoot_FieldWithShipHighHp_ReturnsHit()
    {
        //Arrange
        int x = 1, y = 1;
        const int shipLength = 5;

        var ship = new Destroyer();
        var field = new Field
        {
            Ship = ship
        };
        ship.Fields.AddRange(Enumerable.Range(0, shipLength).Select(_ => new Field()));

        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.GetField(x, y)).Returns(field);
        var game = new Game(boardMock.Object);

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
        var ship = new Destroyer();
        var field = new Field
        {
            Ship = ship
        };
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.GetField(x, y)).Returns(field);
        var game = new Game(boardMock.Object);

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
        var field = new Field
        {
            Ship = new Destroyer(),
            IsHit = true
        };
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.GetField(x, y)).Returns(field);
        var game = new Game(boardMock.Object);

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
        boardMock.Setup(b => b.GetField(x, y)).Returns(field);
        var game = new Game(boardMock.Object);

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
            Ship = new Destroyer()
        };
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.GetField(x, y)).Returns(field);
        var game = new Game(boardMock.Object);

        //Act
        game.Shoot(x, y);

        //Assert
        field.IsHit.Should().BeTrue();
    }

    [Test]
    public void Shoot_FieldWithShip_ShipFieldIsHit()
    {
        //Arrange
        int x = 1, y = 1;
        Ship ship = new Destroyer();
        var field = new Field
        {
            Ship = ship
        };
        ship.Fields.Add(field);
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.GetField(x, y)).Returns(field);
        var game = new Game(boardMock.Object);

        //Act
        game.Shoot(x, y);

        //Assert
        ship.Fields.Count(f => f.IsHit).Should().Be(1);
    }

    #endregion

    #region IsGameWonTests

    [Test]
    public void IsGameWon_AllShipsDestroyed_ReturnsTrue()
    {
        //Arrange
        var ship1 = GetShipWithAllFieldsHit();
        var ship2 = GetShipWithAllFieldsHit();
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(x => x.Ships).Returns(new[] { ship1, ship2 });
        var game = new Game(boardMock.Object);

        //Act
        var isGameWon = game.IsWon();

        //Assert
        isGameWon.Should().BeTrue();

        static Ship GetShipWithAllFieldsHit()
        {
            var ship = new Destroyer();
            ship.Fields.Add(
                new Field
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
        var ship1 = GetShipWithNoFieldsHit();
        var ship2 = GetShipWithNoFieldsHit();
        var game = new GameBuilder()
            .WithShip(ship1)
            .WithShip(ship2)
            .Build();

        //Act
        var isGameWon = game.IsWon();

        //Assert
        isGameWon.Should().BeFalse();
    }

    [Test]
    public void IsGameWon_SomeShipsDestroyed_ReturnsFalse()
    {
        //Arrange
        var ship1 = GetShipWithNoFieldsHit();
        var ship2 = GetShipWithNoFieldsHit();
        var game = new GameBuilder()
            .WithShip(ship1)
            .WithShip(ship2)
            .Build();

        //Act
        var isGameWon = game.IsWon();

        //Assert
        isGameWon.Should().BeFalse();
    }

    private static Ship GetShipWithNoFieldsHit()
    {
        var ship = new Destroyer();
        ship.Fields.Add(new Field());
        return ship;
    }

    #endregion
}