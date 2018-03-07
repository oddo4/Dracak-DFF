using MainDFF.Classes.Battle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.FileHelper
{
    public class DataFileLists
    {
        public List<string> playerCurrentPartyIDList = new List<string>() { "15", "07", "10" };
        public List<PlayerCharacter> playerBasicData = new List<PlayerCharacter>();
        public List<CharacterStats> playerLoadedStats { get; set; }

        public List<EnemyCharacter> enemyBasicData = new List<EnemyCharacter>();
        public void SetBasicPlayerFiles(List<PlayerCharacter> playerList)
        {
            playerBasicData = playerList;
        }
        public void SetBasicEnemyFiles(List<EnemyCharacter> enemyList)
        {
            enemyBasicData = enemyList;
        }
        public List<PlayerCharacter> AssemblePartyCharacter()
        {
            List<PlayerCharacter> list = new List<PlayerCharacter>();

            for (int i = 0; i < playerCurrentPartyIDList.Count; i++)
            {
                var id = int.Parse("00"); //playerCurrentPartyIDList[i]
                var player = playerBasicData[id];
                player.CharacterStats = playerLoadedStats[id];
                player.CharacterStatus = new CharacterStatus(player.CharacterStats);
                list.Add(player);
            }

            return list;
        }
        public List<EnemyCharacter> AssembleEnemyCharacter()
        {
            List<EnemyCharacter> list = new List<EnemyCharacter>();
            Random rand = new Random();
            for (int i = 0; i < 3; i++)
            {
                var enemy = enemyBasicData[0];
                enemy.CharacterStatus = new CharacterStatus(enemy.CharacterStats);

                list.Add(enemy);
            }

            return list;
        }
    }
}
