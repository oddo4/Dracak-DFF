using MainDFF.Classes.ControlActions.MoveActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MainDFF.Classes.Exploration
{
    public class ConflictChecker
    {
        public bool PlayerConflict(AMoveAction character, List<List<int>> ListMap)
        {
            if (ListMap[(int)character.Pos.X][(int)character.Pos.Y] == 1)
            {
                return true;
            }
            return false;
        }
        public bool EnemyConflict(AMoveAction character, List<List<int>> ListMap)
        {
            if (ListMap[(int)character.Pos.X][(int)character.Pos.Y] == 2)
            {
                return true;
            }
            return false;
        }

        public bool PlayerNextLevel(AMoveAction character, List<List<int>> ListMap)
        {
            if (ListMap[(int)character.Pos.X][(int)character.Pos.Y] == 3)
            {
                return true;
            }
            return false;
        }
        public bool WallConflict(AMoveAction character, List<List<int>> ListMap)
        {
            if (ListMap[(int)character.Pos.X][(int)character.Pos.Y] == 5)
            {
                return true;
            }
            return false;
        }
        public bool PortalConflict(AMoveAction character, List<List<int>> ListMap)
        {
            if (ListMap[(int)character.Pos.X][(int)character.Pos.Y] == 4)
            {
                return true;
            }
            return false;
        }

        public bool PortalInteract(PlayerMoveAction player, List<List<int>> ListMap)
        {
            var pos = player.Pos;
            switch (player.Direction)
            {
                case Key.Up:
                    pos.Y -= 1;
                    break;
                case Key.Down:
                    pos.Y += 1;
                    break;
                case Key.Left:
                    pos.X -= 1;
                    break;
                case Key.Right:
                    pos.X += 1;
                    break;
            }

            if (ListMap[(int)pos.X][(int)pos.Y] == 4)
            {
                return true;
            }
            return false;
        }
    }
}
