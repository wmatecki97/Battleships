using Battleships.Console.Models;

namespace Battleships.Console.Interfaces;

public interface IInputTranslator
{
    bool TryGetCoordinatesFromInput(string input, out Coordinate coordinate);
}