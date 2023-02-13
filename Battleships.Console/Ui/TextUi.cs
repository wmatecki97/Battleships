using Battleships.Console.Interfaces;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;

namespace Battleships.Console.Ui;

internal sealed class TextUi
{
    private readonly IGame _game;
    private readonly IInputTranslator _inputTranslator;
    private readonly IMessenger _messenger;
    private bool _isRunning = true;

    internal TextUi(IGame game, IMessenger messenger, IInputTranslator inputTranslator)
    {
        _game = game;
        _messenger = messenger;
        _inputTranslator = inputTranslator;
    }

    internal void Run()
    {
        _messenger.Write("Please type the field coordinates you want to shoot e.g. A1");
        while (_isRunning)
        {
            ProcessNextRound();
        }
    }

    internal void ProcessNextRound()
    {
        var input = _messenger.GetInput();
        if (_inputTranslator.TryGetCoordinatesFromInput(input, out var coordinates))
        {
            var shootStatus = _game.Shoot(coordinates.X, coordinates.Y);
            var message = GetHitResultMessage(shootStatus);
            _messenger.Write(message);

            if (shootStatus == EShootResult.HitAndSunk && _game.IsWon())
            {
                _messenger.Write("You won!");
                _isRunning = false;
            }
        }
        else
        {
            _messenger.Write(
                $"Field coordinates should be a character A-{(char)('A' + _game.Board.Size - 1)} followed by a number 0-{_game.Board.Size - 1} e.g. a1");
        }
    }

    private static string GetHitResultMessage(EShootResult status)
    {
        return status switch
        {
            EShootResult.Hit => "Hit!",
            EShootResult.Miss => "Miss...",
            EShootResult.HitAndSunk => "Hit and sunk",
            EShootResult.AlreadyHit => "Already hit",
            _ => string.Empty
        };
    }
}