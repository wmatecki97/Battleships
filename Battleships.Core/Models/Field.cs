using Battleships.Core.Models.Ships;

namespace Battleships.Core.Models;

public sealed class Field
{
    public Ship? Ship { get; set; }

    public bool IsHit { get; set; }
}