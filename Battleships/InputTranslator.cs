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
            var x = input.First() - 'A';//todo 
            return (x, 0);
        }

        private void ValidateInput(string input)
        {
            if (!Regex.IsMatch(input, $"[A-Z][0-9]"))//todo
                throw new InvalidInputException();
        }
    }
}
