namespace Battleships.Console;

public interface IInputTranslator
{
    bool TryGetCoordinatesFromInput(string input, out Coordinates coordinates);
}