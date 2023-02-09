using Battleships.Console.Exceptions;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Moq;

namespace Battleships.Console.Tests;

[TestFixture]
public class UiTests
{
    [Test]
    public void ProcessNextRound_ValidField_RunsGameLogic()
    {
        var gameMock = new Mock<IGame>();
        var messengerMock = new Mock<IMessenger>();
        const string validField = "A0";
        messengerMock.Setup(m => m.GetInput()).Returns(validField);

        var inputTranslator = new Mock<IInputTranslator>();
        inputTranslator.Setup(t => t.GetCoordinatesFromInput(It.IsAny<string>())).Returns((0, 0));

        var ui = new Ui(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        ui.ProcessNextRound();

        gameMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
    }

    [Test]
    public void ProcessNextRound_InvalidField_DontRunGameLogic()
    {
        const string input = "A0";
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Board).Returns(new Mock<IBoard>().Object);
        var messengerMock = new Mock<IMessenger>();
        messengerMock.Setup(m => m.GetInput()).Returns(input);

        var inputTranslator = new Mock<IInputTranslator>();
        inputTranslator.Setup(t => t.GetCoordinatesFromInput(It.IsAny<string>())).Throws<InvalidInputException>();

        var ui = new Ui(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        ui.ProcessNextRound();

        gameMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
    }

    [Test]
    public void ProcessNextRound_HitAdWon_WritesWonMessage()
    {
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.HitAndSunk);
        gameMock.Setup(l => l.IsGameWon()).Returns(true);

        var messengerMock = new Mock<IMessenger>();
        var inputTranslator = new Mock<IInputTranslator>();

        var ui = new Ui(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        ui.ProcessNextRound();

        gameMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsRegex("Hit and sunk")), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsRegex("You won!")), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsAny<string>()), Times.Exactly(2));
    }

    [Test]
    public void Shoot_Hit_WritesHitMessage()
    {
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.Hit);
        gameMock.Setup(l => l.IsGameWon()).Returns(false);

        var messengerMock = new Mock<IMessenger>();
        var inputTranslator = new Mock<IInputTranslator>();

        var ui = new Ui(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        ui.Shoot(0, 0);

        gameMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsRegex("Hit!")), Times.Once());
    }

    [Test]
    public void Shoot_Miss_WritesMissMessage()
    {
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.Miss);
        gameMock.Setup(l => l.IsGameWon()).Returns(false);

        var messengerMock = new Mock<IMessenger>();
        var inputTranslator = new Mock<IInputTranslator>();

        var ui = new Ui(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        ui.Shoot(0, 0);

        gameMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsRegex("Miss")), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once());
    }

    [Test]
    public void Shoot_AlreadyHitField_WritesAlreadyHitMessage()
    {
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.AlreadyHit);
        gameMock.Setup(l => l.IsGameWon()).Returns(false);

        var messengerMock = new Mock<IMessenger>();
        var inputTranslator = new Mock<IInputTranslator>();

        var ui = new Ui(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        ui.Shoot(0, 0);

        gameMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsRegex("Already hit")), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once());
    }
}