using MainDFF.Classes.ControlActions.MoveActions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainDFF.Classes.Exploration
{
    class MapData
    {
        List<List<int>> ListMap = new List<List<int>>();

        public MapData()
        {
            CreateListMap();
        }
        private void CreateListMap()
        {
            for (int i = 0; i < 16; i++)
            {
                ListMap.Add(new List<int>());
                for (int j = 0; j < 16; j++)
                {
                    ListMap[i].Add(0);
                }
            }
        }

        public void SetPlayerOnMapData(PlayerMoveAction player)
        {
            SetZeroOnMapData(player.LastPos);
            ListMap[(int)player.Pos.X][(int)player.Pos.Y] = 2;
        }
        public void SetEnemyOnMapData(EnemyMoveAction enemy)
        {
            SetZeroOnMapData(enemy.LastPos);
            ListMap[(int)enemy.Pos.X][(int)enemy.Pos.Y] = 1;
        }
        public void SetZeroOnMapData(Point pos)
        {
            ListMap[(int)pos.X][(int)pos.Y] = 0;
        }
        public string TestMap()
        {
            string stringMap = "";
            for (int i = 0; i < ListMap.Count; i++)
            {
                for (int j = 0; j < ListMap[i].Count; j++)
                {
                    stringMap += ListMap[j][i];
                }
                stringMap += Environment.NewLine;
            }

            return stringMap;
        }
    }
}
