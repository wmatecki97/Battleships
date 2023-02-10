using System.Collections.Generic;
using Battleships.Core.Interfaces;

namespace Battleships.Core.Models.Ships;

public abstract class Ship : IShip
{
    protected Ship()
    {
        Fields = new List<Field>();
    }

    public List<Field> Fields { get; }

    public abstract int Length { get; }
}