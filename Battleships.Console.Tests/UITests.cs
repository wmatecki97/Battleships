using Battleships.Console.Interfaces;
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
        //Arrange
        var gameMock = new Mock<IGame>();
        var messengerMock = new Mock<IMessenger>();
        const string validField = "B3";
        const int xCoordinate = 1;
        const int yCoordinate = 3;
        messengerMock.Setup(m => m.GetInput()).Returns(validField);

        var inputTranslatorMock = new Mock<IInputTranslator>();
        var coordinates = new Coordinates
        {
            X = xCoordinate,
            Y = yCoordinate
        };

        const bool isInputValid = true;
        inputTranslatorMock.Setup(t => t.TryGetCoordinatesFromInput(It.IsAny<string>(), out coordinates))
            .Returns(isInputValid);

        var ui = new TextUi(gameMock.Object, messengerMock.Object, inputTranslatorMock.Object);

        //Act
        ui.ProcessNextRound();

        //Assert
        gameMock.Verify(l => l.Shoot(
                    It.Is<int>(x => x == xCoordinate),
                    It.Is<int>(y => y == yCoordinate)),
            Times.Once);
    }

    [Test]
    public void ProcessNextRound_InvalidField_DontRunGameLogic()
    {
        //Arrange
        const string input = "A0";
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Board).Returns(new Mock<IBoard>().Object);
        var messengerMock = new Mock<IMessenger>();
        messengerMock.Setup(m => m.GetInput()).Returns(input);

        var coordinates = new Coordinates();
        var inputTranslator = new Mock<IInputTranslator>();
        const bool isInputValid = false;
        inputTranslator.Setup(t => t.TryGetCoordinatesFromInput(It.IsAny<string>(), out coordinates))
            .Returns(isInputValid);

        var ui = new TextUi(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        //Act
        ui.ProcessNextRound();

        //Assert
        gameMock.Verify(g => g.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
    }

    [Test]
    public void ProcessNextRound_InvalidField_ReturnsInvalidInputMessage()
    {
        //Arrange
        const string input = "A0";
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Board).Returns(new Mock<IBoard>().Object);
        var messengerMock = new Mock<IMessenger>();
        messengerMock.Setup(m => m.GetInput()).Returns(input);

        var coordinates = new Coordinates();
        var inputTranslator = new Mock<IInputTranslator>();
        const bool isInputValid = false;
        inputTranslator.Setup(t => t.TryGetCoordinatesFromInput(It.IsAny<string>(), out coordinates))
            .Returns(isInputValid);

        var ui = new TextUi(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        //Act
        ui.ProcessNextRound();

        //Assert
        gameMock.Verify(g => g.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
    }

    [Test]
    public void ProcessNextRound_HitAdWon_WritesHitAndSunkAndWonMessages()
    {
        //Arrange
        var gameMock = new Mock<IGame>();
        gameMock.Setup(game => game.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.HitAndSunk);
        gameMock.Setup(game => game.IsWon()).Returns(true);

        var messengerMock = new Mock<IMessenger>();
        messengerMock.Setup(m => m.Write(It.IsAny<string>()));

        //todo builder
        var inputTranslator = new Mock<IInputTranslator>();
        const bool isInputValid = true;
        var coordinates = new Coordinates();
        inputTranslator.Setup(i => i.TryGetCoordinatesFromInput(It.IsAny<string>(), out coordinates))
            .Returns(isInputValid);

        var ui = new TextUi(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        //Act
        ui.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write(It.IsRegex("Hit and sunk")), Times.Once());
        messengerMock.Verify(m => m.Write(It.IsRegex("You won!")), Times.Once());
    }

    [Test]
    public void Shoot_Hit_WritesHitMessage()
    {
        //Arrange
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.Hit);

        var messengerMock = new Mock<IMessenger>();
        var inputTranslator = GetInputTranslatorMock(true);

        var ui = new TextUi(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        //Act
        ui.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write(It.IsRegex("Hit!")), Times.Once());
    }

    [Test]
    public void Shoot_Miss_WritesMissMessage()
    {
        //Arrange
        var gameMock = new Mock<IGame>();
        gameMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.Miss);

        var messengerMock = new Mock<IMessenger>();
        var inputTranslator = GetInputTranslatorMock(true);

        var ui = new TextUi(gameMock.Object, messengerMock.Object, inputTranslator.Object);

        //Act
        ui.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write(It.IsRegex("Miss")), Times.Once());
    }

    [Test]
    public void Shoot_AlreadyHitField_WritesAlreadyHitMessage()
    {
        //Arrange
        var boardMock = new Mock<IBoard>();
        boardMock.Setup(x => x.Size);
        var gameMock = new Mock<IGame>();
        gameMock.Setup(g => g.Board).Returns(boardMock.Object);
        gameMock.Setup(g => g.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.AlreadyHit);
        var messengerMock = new Mock<IMessenger>();
        var inputTranslatorMock = GetInputTranslatorMock(true);

        var ui = new TextUi(gameMock.Object, messengerMock.Object, inputTranslatorMock.Object);

        //Act
        ui.ProcessNextRound();

        //Assert
        messengerMock.Verify(m => m.Write(It.IsRegex("Already hit")), Times.Once());
    }

    private static Mock<IInputTranslator> GetInputTranslatorMock(bool succeeded)
    {
        var inputTranslator = new Mock<IInputTranslator>();
        Coordinates coordinates = new();
        inputTranslator.Setup(i => i.TryGetCoordinatesFromInput(It.IsAny<string>(), out coordinates))
            .Returns(succeeded);
        return inputTranslator;
    }
}