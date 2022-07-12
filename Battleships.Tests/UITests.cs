using Battleships.Exceptions;
using Battleships.Interfaces;
using Moq;

namespace Battleships.Tests
{
    public class UITests
    {
        [Test]
        public void ProcessNextRound_ValidField_RunsGameLogicForBothPlayerAndComputer()
        {
            var logicMock = new Mock<IGameLogic>();
            var messegerMock = new Mock<IMessager>();
            const string validField = "A0";
            messegerMock.Setup(m => m.GetInput()).Returns(validField);

            var inputTranslator = new Mock<IInputTranslator>();
            inputTranslator.Setup(t => t.GetCoordinatesFromInput(It.IsAny<string>())).Returns((0, 0));
            
            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.ProcessNextRound();

            const int PlayersCount = 2;
            logicMock.Verify(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>()), Times.Exactly(PlayersCount));
        }

        [Test]
        public void ProcessNextRound_InvalidField_DontRunGameLogic()
        {
            const string input = "A0";
            var logicMock = new Mock<IGameLogic>();
            var messegerMock = new Mock<IMessager>();
            messegerMock.Setup(m => m.GetInput()).Returns(input);

            var inputTranslator = new Mock<IInputTranslator>();
            inputTranslator.Setup(t => t.GetCoordinatesFromInput(It.IsAny<string>())).Throws<InvalidInputException>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.ProcessNextRound();

            logicMock.Verify(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }

        [Test]
        public void Fire_Hit_WritesHitMessage()
        {
            var logicMock = new Mock<IGameLogic>();
            logicMock.Setup(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            logicMock.Setup(l => l.IsGameWon()).Returns(false);

            var messegerMock = new Mock<IMessager>();
            var inputTranslator = new Mock<IInputTranslator>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.Fire(0,0,"player");

            logicMock.Verify(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex(".*Hit.*")), Times.Once());
        }

        [Test]
        public void Fire_Miss_WritesMissMessage()
        {
            var logicMock = new Mock<IGameLogic>();
            logicMock.Setup(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>())).Returns(false);
            logicMock.Setup(l => l.IsGameWon()).Returns(false);

            var messegerMock = new Mock<IMessager>();
            var inputTranslator = new Mock<IInputTranslator>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            ui.Fire(0, 0, "player");

            logicMock.Verify(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex("Miss")), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsAny<string>()), Times.Once());
        }

        [Test]
        public void Fire_HitAdWon_WritesWonMessageWithPropperPlayerName()
        {
            var logicMock = new Mock<IGameLogic>();
            logicMock.Setup(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>())).Returns(true);
            logicMock.Setup(l => l.IsGameWon()).Returns(true);

            var messegerMock = new Mock<IMessager>();
            var inputTranslator = new Mock<IInputTranslator>();

            var ui = new UI(logicMock.Object, messegerMock.Object, inputTranslator.Object);

            string player = "player";
            ui.Fire(0, 0, player);

            logicMock.Verify(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex("Hit")), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsRegex($"{player} won!")), Times.Once());
            messegerMock.Verify(m => m.Write(It.IsAny<string>()), Times.Exactly(2));
        }
    }
}