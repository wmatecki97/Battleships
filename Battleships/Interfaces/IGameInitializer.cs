using Battleships.Models.Ships;

namespace Battleships.Interfaces
{
    public interface IGameInitializer
    {
        void Initialize(IGame game, IEnumerable<Ship>? ships = null);
    }
}