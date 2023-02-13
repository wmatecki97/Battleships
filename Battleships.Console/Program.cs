using Battleships.Console.Interfaces;
using Battleships.Console.Ui;
using Battleships.Core;
using Battleships.Core.Interfaces;
using Battleships.Core.Models.Boards;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = new ServiceCollection()
    .AddSingleton<IRandomNumberGenerator, RandomNumberGenerator>()
    .AddSingleton<IBoard, DefaultBoard>()
    .AddSingleton<IGame, Game>()
    .AddSingleton<IMessenger, ConsoleMessenger>()
    .AddSingleton<IInputTranslator, InputTranslator>()
    .AddSingleton<TextUi>()
    .BuildServiceProvider();

var uI = serviceProvider.GetService<TextUi>();
uI!.Run();