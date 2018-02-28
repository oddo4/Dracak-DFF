using MainDFF.Classes.ControlActions.MoveActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MainDFF.Classes.Exploration
{
    class SetCharacterOnMap
    {
        public void SetPlayerOnMap(PlayerMoveAction MoveAction, Canvas MapCanvas)
        {
            Canvas.SetLeft(MapCanvas, MoveAction.Pos.X * -26);
            Canvas.SetTop(MapCanvas, MoveAction.Pos.Y * -26);
        }
        public void SetEnemyOnMap(List<EnemyMoveAction> EnemyList, Canvas MapCanvas)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                var EnemyCanvas = (Canvas)MapCanvas.Children[i + 1];

                Canvas.SetLeft(EnemyCanvas, EnemyList[i].BegPos.X + (EnemyList[i].Pos.X * 26));
                Canvas.SetTop(EnemyCanvas, EnemyList[i].BegPos.Y + (EnemyList[i].Pos.Y * 26));
            }
        }
    }
}
