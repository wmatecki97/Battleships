﻿// See https://aka.ms/new-console-template for more information
using Battleships;

var game = new Game();

var uI = new UI(game, new ConsoleMessenger(), new InputTranslator());
uI.Run();