using Battleships.Interfaces;

namespace Battleships
{
    public class UI
    {
        private readonly IGameLogic logic;

        public UI(IGameLogic logic)
        {
            this.logic = logic;
        }

        public bool CheckField(string input)
        {
            //logic.CheckField(0, 0);
            return false;
        }
    }
}
