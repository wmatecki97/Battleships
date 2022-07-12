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
    }
}