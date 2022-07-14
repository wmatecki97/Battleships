using Battleships.Exceptions;
using Battleships.Interfaces;
using System.Text.RegularExpressions;

namespace Battleships
{
    public class InputTranslator : IInputTranslator
    {
        public (int, int) GetCoordinatesFromInput(string input)
        {
            input = input.ToUpper();
            ValidateInput(input);
            var x = input.First() - 'A';
            var y = int.Parse(input.Last().ToString());
            return (x, y);
        }

        private void ValidateInput(string input)
        {
            if (input.Length != 2 || !Regex.IsMatch(input, "[A-Z][0-9]"))
                throw new InvalidInputException();
        }
    }
}
