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
        public List<string> enemyChapterElement = new List<string>();
        public int MaxChapters = 9;
        public int CompletedChapters = 0;
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
                var id = int.Parse(playerCurrentPartyIDList[i]);
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
            for (int i = 0; i < rand.Next(1, 7); i++) //rand.Next(1,7)
            {
                EnemyCharacter enemy = new EnemyCharacter(enemyBasicData[rand.Next(0, 2)]); //rand.Next(0, enemyBasicData.Count)
                enemy.CharacterStatus = new CharacterStatus(enemy.CharacterStats);

                list.Add(enemy);
            }

            return list;
        }
        public void CreateNewStats()
        {
            Random rand = new Random();
            playerLoadedStats = new List<CharacterStats>();
            foreach (PlayerCharacter p in playerBasicData)
            {
                p.CharacterClass.SetStats(p.CharacterStats, rand);
                playerLoadedStats.Add(p.CharacterStats);
            }
        }
    }
}
