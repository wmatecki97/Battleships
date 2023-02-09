using System.Collections.Generic;

namespace Battleships.Core.Models.Ships;

public abstract class Ship
{
    protected Ship()
    {
        Fields = new List<Field>();
    }

    public List<Field> Fields { get; }

    public abstract int Length { get; }
}