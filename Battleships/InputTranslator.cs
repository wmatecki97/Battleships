using System.Text.RegularExpressions;
using Battleships.Exceptions;
using Battleships.Interfaces;

namespace Battleships;

public sealed class InputTranslator : IInputTranslator
{
    public (int, int) GetCoordinatesFromInput(string input)
    {
        input = input.ToUpper();
        ValidateInputThrowException(input);
        var x = input.First() - 'A';
        var y = int.Parse(input.Last().ToString());
        return (x, y);
    }

    private void ValidateInputThrowException(string input)
    {
        if (input.Length != 2 || !Regex.IsMatch(input, "[A-J][0-9]"))
            throw new InvalidInputException();
    }
}