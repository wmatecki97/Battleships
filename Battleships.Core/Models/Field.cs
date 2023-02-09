using Battleships.Core.Interfaces;

namespace Battleships.Core.Models;

public sealed class Field
{
    public IShip? Ship { get; set; }

    public bool IsHit { get; set; }
}