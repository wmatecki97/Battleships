using Battleships.Console;
using Battleships.Core;

var game = new Game();

var uI = new Ui(game, new ConsoleMessenger(), new InputTranslator());
uI.Run();