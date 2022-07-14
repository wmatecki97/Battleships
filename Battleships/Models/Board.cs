using Battleships.Interfaces;

namespace Battleships.Models
{
    public class Board : IBoard
    {
        public Field[] Fields { get; }

        public int Size { get; }

        public Board(int size)
        {
            int fieldsCount = size * size;
            Fields = Enumerable.Range(0, fieldsCount).Select(x => new Field()).ToArray();
            Size = size;
        }

        public Field GetField(int x, int y)
        {
            return Fields[x * Size + y];
        }
    }
}
