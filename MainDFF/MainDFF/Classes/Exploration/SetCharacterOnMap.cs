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
        public void SetPlayerOnMap(PlayerMoveAction MoveAction, Canvas MapCanvas, Canvas CharacterField)
        {
            var PlayerCanvas = (Canvas)CharacterField.Children[0];
            Canvas.SetLeft(PlayerCanvas, MoveAction.BegPos.X + (MoveAction.Pos.X * 26));
            Canvas.SetTop(PlayerCanvas, MoveAction.BegPos.Y + (MoveAction.Pos.Y * 26));

            Canvas.SetLeft(MapCanvas, MoveAction.Pos.X * -26);
            Canvas.SetTop(MapCanvas, MoveAction.Pos.Y * -26);
        }
        public void SetEnemyOnMap(List<EnemyMoveAction> EnemyList, Canvas CharacterField)
        {
            for (int i = 0; i < EnemyList.Count; i++)
            {
                var EnemyCanvas = (Canvas)CharacterField.Children[i + 2];

                Canvas.SetLeft(EnemyCanvas, EnemyList[i].BegPos.X + (EnemyList[i].Pos.X * 26));
                Canvas.SetTop(EnemyCanvas, EnemyList[i].BegPos.Y + (EnemyList[i].Pos.Y * 26));
            }
        }

        public void SetPortalOnMap(Portal Portal, Canvas CharacterField)
        {
            var PortalCanvas = (Canvas)CharacterField.Children[1];
            Canvas.SetLeft(PortalCanvas, 291 + (Portal.PortalPos.X * 26));
            Canvas.SetTop(PortalCanvas, 211 + (Portal.PortalPos.Y * 26));
        }
    }
}
