using Battleships.Interfaces;

namespace Battleships.Models
{
    public class Player
    {
        public IBoard Board { get; set; }
        public List<Ship> Ships { get; set; }
        public string Name { get; set; }
    }
}
