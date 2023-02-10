using Battleships.Console.Ui;
using Battleships.Core;
using Battleships.Core.Models.Boards;

var board = new DefaultBoard();
var game = new Game(board);

var uI = new TextUi(game, new ConsoleMessenger(), new InputTranslator());
uI.Run();