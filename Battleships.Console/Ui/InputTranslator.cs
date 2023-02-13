using System.Text.RegularExpressions;
using Battleships.Console.Interfaces;
using Battleships.Console.Models;

namespace Battleships.Console.Ui;

internal sealed class InputTranslator : IInputTranslator
{
    /// <summary>
    /// Attempts to map the string input into Coordinate object
    /// </summary>
    /// <param name="input">Two characters long input, with letter and number</param>
    /// <param name="coordinate"></param>
    public bool TryGetCoordinatesFromInput(string input, out Coordinate coordinate)
    {
        coordinate = new Coordinate();

        if (!IsInputValid(input))
        {
            return false;
        }

        input = input.ToUpper();
        coordinate.X = input.First() - 'A';
        coordinate.Y = int.Parse(input.Last().ToString());

        return true;
    }

    private static bool IsInputValid(string input)
    {
        return !string.IsNullOrEmpty(input)
               && input.Length == 2
               && Regex.IsMatch(input, "[A-J a-j][0-9]");
    }
}