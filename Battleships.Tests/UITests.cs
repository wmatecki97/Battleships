using Battleships.Interfaces;
using Moq;

namespace Battleships.Tests
{
    public class UITests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckField_ValidField_RunsGameLogic()
        {
            var logic = new Mock<IGameLogic>();
            var ui = new UI(logic.Object);
            const string validField = "A0";

            ui.CheckField(validField);

            logic.Verify(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void CheckField_InvalidField_DontRunGameLogic()
        {
            var logic = new Mock<IGameLogic>();
            var ui = new UI(logic.Object);
            const string validField = "AB";

            ui.CheckField(validField);

            logic.Verify(l => l.CheckField(It.IsAny<int>(), It.IsAny<int>()), Times.Never());
        }
    }
}