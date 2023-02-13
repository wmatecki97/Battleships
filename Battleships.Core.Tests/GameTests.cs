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

    [Test]
    public void Shoot_FieldWithNotHitDestroyer_ReturnsHit()
    {
        //Arrange
        const int x = 1;
        const int y = 1;
        const int shipLength = 5;

        var destroyer = new Destroyer();
        var field = new Field
        {
            Ship = destroyer
        };
        destroyer.Fields.AddRange(Enumerable.Range(0, shipLength).Select(_ => new Field()));

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
        const int x = 1;
        const int y = 1;
        var fieldToBeShoot = new Field();
        var destroyer = new Destroyer
        {
            Fields =
            {
                new Field { IsHit = true },
                new Field { IsHit = true },
                new Field { IsHit = true },
                fieldToBeShoot
            }
        };
        fieldToBeShoot.Ship = destroyer;

        var boardMock = new Mock<IBoard>();
        boardMock.Setup(b => b.GetField(x, y)).Returns(fieldToBeShoot);
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
        const int x = 1;
        const int y = 1;
        var field = new Field
        {
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
        const int x = 1;
        const int y = 1;
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
        const int x = 1;
        const int y = 1;
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
        const int x = 1;
        const int y = 1;
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
    public void IsWon_AllShipsDestroyed_ReturnsTrue()
    {
        //Arrange
        var game = new GameBuilder()
            .WithDestroyedShip()
            .WithDestroyedShip()
            .Build();

        //Act
        bool isGameWon = game.IsWon();

        //Assert
        isGameWon.Should().BeTrue();
    }

    [Test]
    public void IsWon_NoneShipsDestroyed_ReturnsFalse()
    {
        //Arrange
        var game = new GameBuilder()
            .WithNotDestroyedShip()
            .WithNotDestroyedShip()
            .Build();

        //Act
        bool isGameWon = game.IsWon();

        //Assert
        isGameWon.Should().BeFalse();
    }

    [Test]
    public void IsWon_SomeShipsDestroyed_ReturnsFalse()
    {
        //Arrange
        var game = new GameBuilder()
            .WithNotDestroyedShip()
            .WithDestroyedShip()
            .Build();

        //Act
        bool isGameWon = game.IsWon();

        //Assert
        isGameWon.Should().BeFalse();
    }

    #endregion
}