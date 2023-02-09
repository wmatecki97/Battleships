using Battleships.Console;
using Battleships.Core;

var game = new GameLogic();

var uI = new Ui(game, new ConsoleMessenger(), new InputTranslator());
uI.Run();