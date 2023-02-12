using System;
using Battleships.Core.Interfaces;

namespace Battleships.Core;

internal class RandomNumberGenerator : IRandomNumberGenerator
{
    private readonly Random _rand;

    public RandomNumberGenerator()
    {
        _rand = new Random();
    }

    public int GetRandomNumber(int start, int end)
    {
        return _rand.Next(start, end);
    }
}