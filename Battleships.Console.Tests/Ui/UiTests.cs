using Battleships.Console.Tests.Builders;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Moq;

namespace Battleships.Console.Tests.Ui;

[TestFixture]
public class UiTests
{
    [Test]
    public void ProcessNextRound_ValidInput_ShootAtTheFieldGiven()
    {
        //Arrange
        const int xCoordinate = 1;
        const int yCoordinate = 3;
        const bool isInputValid = true;

        var gameMock = new Mock<IGame>();
        var messengerMock = new Mock<IMessenger>();

        var sut = new UiBuilder()
            .WithMessenger(messengerMock.Object)
            .WithMockedInput(isInputValid, xCoordinate, yCoordinate)
            .WithGame(gameMock.Object)
            .Build();

        //Act
        sut.ProcessNextRound();

        //Assert
        gameMock.Verify(l => l.Shoot(
                It.Is<int>(x => x == xCoordinate),
                It.Is<int>(y => y == yCoordinate)),
            Times.Once);
    }

    [Test]
    public void ProcessNextRound_InvalidInput_DontShoot()
    {
        //Arrange
        const bool isInputValid = false;

        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Board).Returns(new Mock<IBoard>().Object);

        var sut = new UiBuilder()
            .WithMockedInput(isInputValid)
            .WithBoard(1)
            .Build();

        //Act
        sut.ProcessNextRound();

        //Assert
        gameMock.Verify(g => g.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
    }

    [Test]
    public void ProcessNextRound_InvalidInput_WriteInvalidInputMessage()
    {
        //Arrange
        const bool isInputValid = false;

        var messengerMock = new Mock<IMessenger>();

        var sut = new UiBuilder()
            .WithBoard(2)
            .WithMockedInput(isInputValid)
            .WithMessenger(messengerMock.Object)
            .Build();


        //Act
        sut.ProcessNextRound();

        //Assert
        messengerMock.Verify(m =>
            m.Write("Field coordinates should be a character A-B followed by a number 0-1 e.g. a1"));
    }

    [Test]
    public void ProcessNextRound_HitAdWon_WritesHitAndSunkAndWonMessages()
    {
        //Arrange
        const bool isInputValid = true;
        const bool isGameWon = true;

        var gameMock = new Mock<IGame>();
        gameMock.Setup(game => game.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.HitAndSunk);
        gameMock.Setup(game => game.IsWon()).Returns(isGameWon);
        var messengerMock = new Mock<IMessenger>();

        var sut = new UiBuilder()
            .WithMockedInput(isInputValid)
            .WithMockedNextShootResult(EShootResult.HitAndSunk)
            .WithMessenger(messengerMock.Object)
            .WithGame(gameMock.Object)
            .Build();

        //Act
        sut.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write("Hit and sunk"), Times.Once());
        messengerMock.Verify(m => m.Write("You won!"), Times.Once());
    }

    [Test]
    public void ProcessNextRound_Hit_WritesHitMessage()
    {
        //Arrange
        const bool isInputValid = true;
        var messengerMock = new Mock<IMessenger>();


        var sut = new UiBuilder()
            .WithMockedInput(isInputValid)
            .WithMockedNextShootResult(EShootResult.Hit)
            .WithMessenger(messengerMock.Object)
            .Build();

        //Act
        sut.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write("Hit!"), Times.Once());
    }

    [Test]
    public void ProcessNextRound_Miss_WritesMissMessage()
    {
        //Arrange
        const bool isInputValid = true;
        var messengerMock = new Mock<IMessenger>();


        var sut = new UiBuilder()
            .WithMockedInput(isInputValid)
            .WithMockedNextShootResult(EShootResult.Miss)
            .WithMessenger(messengerMock.Object)
            .Build();

        //Act
        sut.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write("Miss..."), Times.Once());
    }

    [Test]
    public void ProcessNextRound_AlreadyHitField_WritesAlreadyHitMessage()
    {
        //Arrange
        var messengerMock = new Mock<IMessenger>();
        const bool isInputValid = true;

        var sut = new UiBuilder()
            .WithMockedInput(isInputValid)
            .WithMockedNextShootResult(EShootResult.AlreadyHit)
            .WithMessenger(messengerMock.Object)
            .Build();

        //Act
        sut.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write("Already hit"), Times.Once());
    }
}