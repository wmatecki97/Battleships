using Battleships.Exceptions;
using Battleships.Interfaces;

namespace Battleships
{
    public class UI
    {
        private readonly IGame game;
        private readonly IMessenger messager;
        private readonly IInputTranslator inputTranslator;
        private bool _isRunning = true;

        public UI(IGame game, IMessenger messager, IInputTranslator inputTranslator)
        {
            this.game = game;
            this.messager = messager;
            this.inputTranslator = inputTranslator;
        }

        public void Run()
        {
            messager.Write("Please type the field coordinates you want to shoot e.g. A1");
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
                Shoot(x, y);

                var isGameWon = game.IsGameWon();
                if (isGameWon)
                {
                    messager.Write($"You won!");
                    _isRunning = false;
                }
            }
            catch (InvalidInputException)
            {
                messager.Write($"Field coordinates should be a character followed by a number e.g. a1 in range 0-{game.Board.Size-1}");
            }
        }

        public virtual void Shoot(int x, int y)
        {
            var isHit = game.Shoot(x, y);
            string message = string.Empty;
            switch (isHit)
            {
                case Models.EShootResult.Hit: message = "Hit!"; break;
                case Models.EShootResult.Miss: message = "Miss..."; break;
                case Models.EShootResult.HitAndSunk: message = "Hit and sunk"; break;
                case Models.EShootResult.AlreadyHit: message = "Already hit"; break;
            }
            messager.Write(message);
        }
    }
}
