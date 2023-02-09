using System.Collections.Generic;
using Battleships.Core.Interfaces;
using Battleships.Core.Models.Ships;

namespace Battleships.Core.Models.Boards;

/// <summary>
/// Default Board of size 10x10 that contains Battleship and two Destroyers
/// </summary>
public class DefaultBoard : RandomShipPlacementBoardBaseBase
{
    public DefaultBoard(int size) : base(DefaultShips, size)
    {
    }

    private static readonly IEnumerable<IShip> DefaultShips = new Ship[]
    {
        new Battleship(),
        new Destroyer(),
        new Destroyer()
    };
}