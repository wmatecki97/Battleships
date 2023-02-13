namespace Battleships.Core.Interfaces;

public interface IRandomNumberGenerator
{
    /// <summary>
    ///     Returns the int value in specified range
    /// </summary>
    /// <param name="start">Inclusive lower bound</param>
    /// <param name="end">Exclusive upper bound</param>
    int GetRandomNumber(int start, int end);
}