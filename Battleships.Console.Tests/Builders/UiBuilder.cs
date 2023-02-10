using Battleships.Console.Interfaces;
using Battleships.Console.Models;
using Battleships.Console.Ui;
using Battleships.Core.Interfaces;
using Battleships.Core.Models;
using Moq;

namespace Battleships.Console.Tests.Builders;

internal class UiBuilder
{
    private Mock<IBoard>? _boardMock;
    private IGame? _game;
    private IInputTranslator? _inputTranslator;
    private IMessenger? _messenger;
    private EShootResult? _nextShootResult;

    public UiBuilder WithBoard(int size)
    {
        _boardMock = new Mock<IBoard>();
        _boardMock.Setup(b => b.Size).Returns(size);
        return this;
    }

    public UiBuilder WithGame(IGame game)
    {
        _game = game;
        return this;
    }

    public UiBuilder WithMockedNextShootResult(EShootResult shootResult)
    {
        _nextShootResult = shootResult;
        return this;
    }

    public UiBuilder WithMessenger(IMessenger messenger)
    {
        _messenger = messenger;
        return this;
    }

    public UiBuilder WithMockedInput(bool valid, int xCoordinate = 0, int yCoordinate = 0)
    {
        var coordinates = new Coordinates
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
        _boardMock ??= new Mock<IBoard>();

        if (_game is null)
        {
            var gameMock = new Mock<IGame>();
            gameMock.Setup(g => g.Board).Returns(_boardMock.Object);
            if (_nextShootResult is not null)
            {
                gameMock.Setup(g => g.Shoot(It.IsAny<int>(), It.IsAny<int>()))
                    .Returns(_nextShootResult.Value);
            }

            _game = gameMock.Object;
        }

        _messenger ??= new Mock<IMessenger>().Object;
        _inputTranslator ??= new Mock<IInputTranslator>().Object;

        var ui = new TextUi(_game, _messenger, _inputTranslator);
        return ui;
    }
}