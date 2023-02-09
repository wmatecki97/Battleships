using System.Collections.Generic;

namespace Battleships.Core.Interfaces;

public interface IGameInitializer
{
    void Initialize(IGame game, IEnumerable<IShip>? ships = null);
}