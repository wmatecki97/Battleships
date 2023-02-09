using System.Collections.Generic;
using Battleships.Core.Models.Ships;

namespace Battleships.Core.Interfaces;

public interface IGameInitializer
{
    void Initialize(IGame game, IEnumerable<Ship>? ships = null);
}