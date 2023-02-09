namespace Battleships.Models.Ships;

public class Ship
{
    public Ship(int length)
    {
        Length = length;
        Fields = new List<Field>();
    }

    public int Length { get; }

    public List<Field> Fields { get; }
}