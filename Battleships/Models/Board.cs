using Battleships.Interfaces;

namespace Battleships.Models
{
    public class Board : IBoard
    {
        private Field[] _fields;
        private readonly int size;

        public Board(int size)
        {
            int fieldsCount = size * size;
            _fields = Enumerable.Range(0, fieldsCount).Select(x => new Field()).ToArray();
            this.size = size;
        }

        public Field GetField(int x, int y)
        {
            return _fields[x * size + y];
        }
    }
}
