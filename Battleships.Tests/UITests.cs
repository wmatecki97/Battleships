using Battleships.Exceptions;
using Battleships.Interfaces;
using Battleships.Models;
using Moq;

namespace Battleships.Tests
{
    public class UITests
    {
        [Test]
        public void ProcessNextRound_ValidField_RunsGameLogic()
        {
            var logicMock = new Mock<IGame>();
            var messegerMock = new Mock<IMessager>();
            const string validField = "A0";
            messegerMock.Setup(m => m.GetInput()).Returns(validField);

            var inputTranslator = new Mock<IInputTranslator>();
            inputTranslator.Setup(t => t.GetCoordinatesFromInput(It.IsAny<string>())).Returns((0, 0));
            
            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.ProcessNextRound();

            logicMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void ProcessNextRound_InvalidField_DontRunGameLogic()
        {
            const string input = "A0";
            var logicMock = new Mock<IGame>();
            var messegerMock = new Mock<IMessager>();
            messegerMock.Setup(m => m.GetInput()).Returns(input);

            var inputTranslator = new Mock<IInputTranslator>();
            inputTranslator.Setup(t => t.GetCoordinatesFromInput(It.IsAny<string>())).Throws<InvalidInputException>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.ProcessNextRound();

            logicMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }

        [Test]
        public void Fire_Hit_WritesHitMessage()
        {
            var logicMock = new Mock<IGame>();
            logicMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.Hit);
            logicMock.Setup(l => l.IsGameWon()).Returns(false);

            var messegerMock = new Mock<IMessager>();
            var inputTranslator = new Mock<IInputTranslator>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.Shoot(0,0);

            logicMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex("Hit!")), Times.Once());
        }

        [Test]
        public void Fire_Miss_WritesMissMessage()
        {
            var logicMock = new Mock<IGame>();
            logicMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.Miss);
            logicMock.Setup(l => l.IsGameWon()).Returns(false);

            var messegerMock = new Mock<IMessager>();
            var inputTranslator = new Mock<IInputTranslator>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.Shoot(0, 0);

            logicMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex("Miss")), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Fire_HitAdWon_WritesWonMessage()
        {
            var logicMock = new Mock<IGame>();
            logicMock.Setup(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>())).Returns(EShootResult.HitAndSunk);
            logicMock.Setup(l => l.IsGameWon()).Returns(true);

            var messegerMock = new Mock<IMessager>();
            var inputTranslator = new Mock<IInputTranslator>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.Shoot(0, 0);

            logicMock.Verify(l => l.Shoot(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex("Hit and sunk")), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex($"You won!")), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}