using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes
{
    public class ResourcePaths
    {
        public string PlayerBattlePath = @"pack://application:,,,/Resources/CharacterSprites/Battle/";
        public string PlayerImagePanelPath = @"pack://application:,,,/Resources/CharacterPanels/";
        public string EnemyBattlePath = @"pack://application:,,,/Resources/CharacterSprites/Monsters/";
        public string BattleBackgroundPath = @"pack://application:,,,/Resources/BattleBackgrounds/";
        public string MapPath = @"pack://application:,,,/Resources/Map/";

        public string GetPlayerPath(string id)
        {
            return PlayerBattlePath + id + "/";
        }
        public string GetPlayerImagePath(string id)
        {
            return PlayerImagePanelPath + id + ".png";
        }
        public string GetEnemyPath(string id)
        {
            return EnemyBattlePath + id + "/";
        }

        public string GetBattleBackground(string enemyElement)
        {
            return BattleBackgroundPath + enemyElement + ".png";
        }
        public string GetMapPath(string enemyElement)
        {
            return MapPath + enemyElement + "/Path.png";
        }
        public string GetMapFloor(string enemyElement)
        {
            return MapPath + enemyElement + "/Floor.png";
        }
        public string GetMapWall(string enemyElement)
        {
            return MapPath + enemyElement + "/Wall.png";
        }
        public string GetMapBG(string enemyElement)
        {
            return MapPath + enemyElement + "/BG.png";
        }
    }
}
