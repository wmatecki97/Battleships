using System.Collections.Generic;

namespace Battleships.Core.Interfaces;

public interface IGameInitializer
{
    void Initialize(IBoard game, IEnumerable<IShip>? ships = null);
}