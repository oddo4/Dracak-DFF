using MainDFF.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.Exploration
{
    public class LevelList
    {
        public List<MapData> LevelsList = new List<MapData>();
        public int EnemyCount { get; set; }
        public string EnemyElementID { get; set; }
        
        public LevelList(Random rand, int levelID)
        {
            Dictionary<int, int> switchLast = new Dictionary<int, int>() { { 0, 1 }, { 1, 0 }, { 2, 3 }, { 3, 2 } };
            int levelCount = rand.Next(3, 5);
            var lastDirection = -1;
            var portalBool = false;
            var keyBool = false;
            int portalIndex = rand.Next(0, levelCount);
            int keyIndex = rand.Next(0, levelCount);
            for (int i = 0; i < levelCount; i++)
            {
                var enemyCount = rand.Next(2, 5);
                var pathDirection = rand.Next(0, 4);

                while (pathDirection == lastDirection)
                {
                    pathDirection = rand.Next(0, 4);
                }

                if (i == levelCount - 1)
                {
                    pathDirection = -1;
                }


                if (i == portalIndex)
                {
                    portalBool = true;
                }
                else
                {
                    portalBool = false;
                }
                if (i == keyIndex)
                {
                    keyBool = true;
                }
                else
                {
                    keyBool = false;
                }


                LevelsList.Add(new MapData(i, enemyCount, pathDirection, lastDirection, portalBool, keyBool));

                EnemyCount += enemyCount;
                if (pathDirection != -1)
                {
                    lastDirection = switchLast[pathDirection];
                }
            }
        }
    }
}
