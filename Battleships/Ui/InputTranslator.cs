using System.Text.RegularExpressions;
using Battleships.Console.Interfaces;
using Battleships.Console.Models;

namespace Battleships.Console.Ui;

internal sealed class InputTranslator : IInputTranslator
{
    /// <summary>
    /// Attempts to map the string input into Coordinates object
    /// </summary>
    /// <param name="input">Two characters long input, with letter and number</param>
    /// <param name="coordinates"></param>
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