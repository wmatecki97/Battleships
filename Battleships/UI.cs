using Battleships.Exceptions;
using Battleships.Interfaces;
using Battleships.Models;

namespace Battleships;

public class Ui
{
    private readonly IGame _game;
    private readonly IInputTranslator _inputTranslator;
    private readonly IMessenger _messenger;
    private bool _isRunning = true;

    public Ui(IGame game, IMessenger messenger, IInputTranslator inputTranslator)
    {
        _game = game;
        _messenger = messenger;
        _inputTranslator = inputTranslator;
    }

    public void Run()
    {
        _messenger.Write("Please type the field coordinates you want to shoot e.g. A1");
        while (_isRunning)
        {
            ProcessNextRound();
        }
    }

    public void ProcessNextRound()
    {
        try
        {
            string input = _messenger.GetInput();
            (int x, int y) = _inputTranslator.GetCoordinatesFromInput(input);
            Shoot(x, y);

            var isGameWon = _game.IsGameWon();
            if (isGameWon)
            {
                _messenger.Write("You won!");
                _isRunning = false;
            }
        }
        catch (InvalidInputException)
        {
            _messenger.Write(
                $"Field coordinates should be a character A-{(char)('A' + _game.Board.Size - 1)} followed by a number 0-{_game.Board.Size - 1} e.g. a1");
        }
    }

    public virtual void Shoot(int x, int y)
    {
        var isHit = _game.Shoot(x, y);
        string message = string.Empty;
        switch (isHit)
        {
            case EShootResult.Hit:
                message = "Hit!";
                break;
            case EShootResult.Miss:
                message = "Miss...";
                break;
            case EShootResult.HitAndSunk:
                message = "Hit and sunk";
                break;
            case EShootResult.AlreadyHit:
                message = "Already hit";
                break;
        }

        _messenger.Write(message);
    }
}