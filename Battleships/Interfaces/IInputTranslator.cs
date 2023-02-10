namespace Battleships.Console.Interfaces;

public interface IInputTranslator
{
    bool TryGetCoordinatesFromInput(string input, out Coordinates coordinates);
}