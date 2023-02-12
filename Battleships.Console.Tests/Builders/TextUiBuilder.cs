using Battleships.Console.Interfaces;
using Battleships.Console.Models;
using Battleships.Console.Ui;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Moq;

namespace Battleships.Console.Tests.Builders;

internal class TextUiBuilder
{
    private IBoard? _board;
    private IGame? _game;
    private IInputTranslator? _inputTranslator;
    private IMessenger? _messenger;
    private EShootResult? _nextShootResult;

    public TextUiBuilder WithMockedBoard(int size)
    {
        var board = new Mock<IBoard>();
        board.Setup(b => b.Size).Returns(size);
        _board = board.Object;
        return this;
    }

    public TextUiBuilder WithGame(IGame game)
    {
        _game = game;
        return this;
    }

    public TextUiBuilder WithMockedNextShootResult(EShootResult shootResult)
    {
        _nextShootResult = shootResult;
        return this;
    }

    public TextUiBuilder WithMessenger(IMessenger messenger)
    {
        _messenger = messenger;
        return this;
    }

    public TextUiBuilder WithMockedInput(bool valid, int xCoordinate = 0, int yCoordinate = 0)
    {
        var coordinates = new Coordinate
        {
            X = xCoordinate,
            Y = yCoordinate
        };

        var inputTranslatorMock = new Mock<IInputTranslator>();
        inputTranslatorMock
            .Setup(x => x.TryGetCoordinatesFromInput(It.IsAny<string>(), out coordinates))
            .Returns(valid);
        _inputTranslator = inputTranslatorMock.Object;

        return this;
    }

    public TextUi Build()
    {
        _board ??= new Mock<IBoard>().Object;

        if (_game is null)
        {
            var gameMock = new Mock<IGame>();
            gameMock.Setup(g => g.Board).Returns(_board);
            if (_nextShootResult is not null)
            {
                gameMock.Setup(g => g.Shoot(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(_nextShootResult.Value);
            }

            _game = gameMock.Object;
        }

        _messenger ??= new Mock<IMessenger>().Object;
        _inputTranslator ??= new Mock<IInputTranslator>().Object;

        return new TextUi(_game, _messenger, _inputTranslator);
    }
}