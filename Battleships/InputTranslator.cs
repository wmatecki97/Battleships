using System.Text.RegularExpressions;
using Battleships.Core.Interfaces;

namespace Battleships.Console;

internal sealed class InputTranslator : IInputTranslator
{
    public bool TryGetCoordinatesFromInput(string input, out Coordinates coordinates)
    {
        coordinates = new Coordinates();

        if (!IsInputValid(input))
        {
            return false;
        }
        input = input.ToUpper();
        coordinates.X = input.First() - 'A';
        coordinates.Y = int.Parse(input.Last().ToString());

        return true;
    }

    private bool IsInputValid(string input)
    {
        return !string.IsNullOrEmpty(input)
            && input.Length == 2 
            && Regex.IsMatch(input, "[A-J a-j][0-9]");
    }
}