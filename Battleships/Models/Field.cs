namespace Battleships.Models
{
    public class Field
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Ship? Ship { get; set; }
        public bool IsHit { get; set; }
    }
}
