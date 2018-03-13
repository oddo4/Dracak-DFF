using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes
{
    public class ResourcePaths
    {
        public string PlayerBattlePath = "pack://application:,,,/Resources/CharacterSprites/Battle/";
        public string EnemyBattlePath = "pack://application:,,,/Resources/CharacterSprites/Monsters/";

        public string GetPlayerPath(string id)
        {
            return PlayerBattlePath + id + "/";
        }
        public string GetEnemyPath(string id)
        {
            return EnemyBattlePath + id + "/";
        }
    }
}
