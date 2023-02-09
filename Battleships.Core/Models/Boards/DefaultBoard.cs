using System.Collections.Generic;
using Battleships.Core.Interfaces;
using Battleships.Core.Models.Ships;

namespace Battleships.Core.Models.Boards;

/// <summary>
/// Default Board of DefaultSize 10x10 that contains Battleship and two Destroyers
/// </summary>
public class DefaultBoard : RandomShipPlacementBoardBase
{
    public DefaultBoard() : base(DefaultShips, 10)
    {
    }

    private static int DefaultSize => 10;
    private static readonly IEnumerable<IShip> DefaultShips = new Ship[]
    {
        new Battleship(),
        new Destroyer(),
        new Destroyer()
    };
}