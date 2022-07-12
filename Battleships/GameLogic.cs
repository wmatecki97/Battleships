using Battleships.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Battleships
{
    internal class GameLogic : IGameLogic
    {
        public int BoardSize => throw new NotImplementedException();

        public bool CheckField(int x, int y)
        {
            return false;
        }

        public bool IsGameWon()
        {
            throw new NotImplementedException();
        }
    }
}
