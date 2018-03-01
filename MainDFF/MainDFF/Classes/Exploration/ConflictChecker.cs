using MainDFF.Classes.ControlActions.MoveActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Exploration
{
    public class ConflictChecker
    {
        public bool Conflict(AMoveAction character, List<List<int>> ListMap)
        {
            if (ListMap[(int)character.Pos.X][(int)character.Pos.Y] != 0)
            {
                return true;
            }
            return false;
        }
    }
}
