namespace Battleships.Core.Interfaces;

public interface IInputTranslator
{
    (int, int) GetCoordinatesFromInput(string input);
}