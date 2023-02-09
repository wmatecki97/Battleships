namespace Battleships.Interfaces;

public interface IInputTranslator
{
    (int, int) GetCoordinatesFromInput(string input);
}