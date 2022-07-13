using Battleships.Exceptions;
using Battleships.Interfaces;

namespace Battleships
{
    public class UI
    {
        private readonly IGame logic;
        private readonly IMessager messager;
        private readonly IInputTranslator inputTranslator;
        private bool _isRunning = true;

        public UI(IGame logic, IMessager messager, IInputTranslator inputTranslator)
        {
            this.logic = logic;
            this.messager = messager;
            this.inputTranslator = inputTranslator;
        }

        public void Run()
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

                const string playerName = "player";

                Fire(x, y, playerName);

                const string computerName = "computer";

                Fire(0, 0, computerName);//todo logic for coordinates
            }
            catch (InvalidInputException)
            {
                messager.Write("Field coordinates should be a character followed by a number e.g. a1");
            }
        }

        public void Fire(int x, int y, string playerName)
        {
            var isHit = logic.Shoot(x, y); //todo struct with more info
            string message = string.Empty;
            switch (isHit)
            {
                case Models.EShootResult.Hit: message = "Hit!"; break;
                case Models.EShootResult.Miss: message = "Miss..."; break;
                case Models.EShootResult.HitAndSunk: message = "Hit and sunk"; break;
            }
            messager.Write(message);

            var isGameWon = logic.IsGameWon();
            if (isGameWon)
            {
                messager.Write($"{playerName} won!");
                _isRunning = false;
            }
        }
    }
}
