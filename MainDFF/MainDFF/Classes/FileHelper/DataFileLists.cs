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
        public List<string> playerIDList = new List<string>();
        public List<string> playerCurrentPartyIDList = new List<string>();
        public List<string> playerClassIDList = new List<string>();
        public List<string> playerNameList = new List<string>();
        public CharacterStats playerBasicStats { get; set; }
        List<List<CharacterAnimation>> playerCharAnimList = new List<List<CharacterAnimation>>();
        public List<string> playerClassNameList = new List<string>();
        public List<CharacterStats> playerCurrentStats = new List<CharacterStats>();

        public List<int> enemyIDList = new List<int>();

        public void GetBasicFiles(List<string> idList, List<string> classIdList, List<string> nameList, List<CharacterStats> statsList, List<List<CharacterAnimation>> charAnimList, List<string> classNameList)
        {
            playerIDList = idList;
            playerClassIDList = classIdList;
            playerNameList = nameList;
            playerBasicStats = statsList[0];
            playerClassNameList = classNameList;
            playerCharAnimList = charAnimList;
            CreateCurrentStats();
        }
        public void CreateCurrentStats()
        {
            List<string> list = new List<string>() { "15", "10", "07" };
            for (int j = 0; j < list.Count; j++)
            {
                playerCurrentPartyIDList.Add(list[j]);
            }
            for (int i = 0; i < playerClassIDList.Count; i++)
            {
                playerCurrentStats.Add(playerBasicStats);
            }
        }
        public List<PlayerCharacter> AssemblePartyCharacter()
        {
            List<PlayerCharacter> list = new List<PlayerCharacter>();

            for (int i = 0; i < playerCurrentPartyIDList.Count; i++)
            {
                var playerID = int.Parse(playerCurrentPartyIDList[i]);
                PlayerCharacter p = new PlayerCharacter();
                p.CharacterID = playerCurrentPartyIDList[i];
                p.ClassID = int.Parse(playerClassIDList[playerID]);
                p.CharacterClass = p.SetClass();
                p.Name = playerNameList[playerID];
                p.CharacterStats = playerCurrentStats[playerID];
                p.CharacterAnimationList = playerCharAnimList[playerID];
                p.CharacterClass.ClassName = playerClassNameList[p.ClassID];
                list.Add(p);
            }

            return list;
        }
    }
}
