using Battleships.Console;
using Battleships.Core;
using Battleships.Core.Models.Boards;

var board = new DefaultBoard();
var game = new GameLogic(board);

var uI = new Ui(game, new ConsoleMessenger(), new InputTranslator());
uI.Run();