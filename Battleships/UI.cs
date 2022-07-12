using Battleships.Exceptions;
using Battleships.Interfaces;
using System.Text.RegularExpressions;

namespace Battleships
{
    public class UI
    {
        private readonly IGameLogic logic;
        private readonly IMessager messager;
        private readonly IInputTranslator inputTranslator;
        private bool _isRunning = true;

        public UI(IGameLogic logic, IMessager messager, IInputTranslator inputTranslator)
        {
            this.logic = logic;
            this.messager = messager;
            this.inputTranslator = inputTranslator;
        }

        public async void Run()
        {
            while (_isRunning)
            {
                ProcessNextRound();
            }
        }

        public void ProcessNextRound()
        {
            try
            {
                string input = messager.GetInput();
                (int x, int y) = inputTranslator.GetCoordinatesFromInput(input);

                string playerName = "player";

                var isHit = logic.CheckField(x, y); //todo struct with more info
                var message = isHit ? "Hit!" : "Miss";
                messager.Write(message);

                var isGameWon = logic.IsGameWon();
                if (isGameWon)
                {
                    messager.Write($"{playerName} won!");
                    _isRunning = false;
                }

                //todo computer
            }
            catch (InvalidInputException)
            {
                messager.Write("Field coordinates should be a character followed by a number e.g. a1");
            }
        }
    }
}
