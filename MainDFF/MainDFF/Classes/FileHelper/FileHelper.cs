using MainDFF.Classes.Battle;
using MainDFF.Classes.Battle.AttackBehaviors;
using MainDFF.Classes.Battle.CharacterClass;
using MainDFF.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MainDFF.Classes.FileHelper
{
    public class FileHelper
    {
        public string DataFilesPath = AppDomain.CurrentDomain.BaseDirectory + "/DataFiles/";
        public string SaveDataPath = AppDomain.CurrentDomain.BaseDirectory + "/SaveData/";
        public int SaveSlot = 0;
        public FileHelper()
        {
            App.dataFileLists.SetBasicPlayerFiles(ReadPlayerFile(DataFilesPath, "PlayerBasicData"));
            if (!Directory.Exists(SaveDataPath))
            {
                for (int i = 0; i < 3; i++)
                {
                    Directory.CreateDirectory(SaveDataPath + i);
                }
            }
        }
        public string LoadEnemyElement(int id)
        {
            var result = ReadStringFile(DataFilesPath, "EnemyElementID");
            return result[id];
        }
        public void LoadEnemyData(string dirID)
        {
            App.dataFileLists.SetBasicEnemyFiles(ReadEnemyFile(DataFilesPath + "/EnemyFiles/" + dirID + "/", "EnemyData"));
        }
        public bool LoadData()
        {
            var partyID = ReadStringFile(SaveDataPath + "/" + SaveSlot + "/", "SavedPartyID");
            var stats = ReadCharacterStatsFile(SaveDataPath + "/" + SaveSlot + "/", "SavedStats");
            var chapter = ReadStringFile(SaveDataPath + "/" + SaveSlot + "/", "SavedCurrentChapter").FirstOrDefault();
            if (partyID != null || stats != null)
            {
                App.dataFileLists.playerCurrentPartyIDList = partyID;
                App.dataFileLists.playerLoadedStats = stats;
                App.dataFileLists.CompletedChapters = int.Parse(chapter);

                return true;
            }
            return false;
        }
        public bool SaveData()
        {
            var partyID = App.dataFileLists.playerCurrentPartyIDList;
            var stats = App.dataFileLists.playerLoadedStats;

            if (WriteStringFile(partyID, SaveDataPath + "/" + SaveSlot + "/", "SavedPartyID") 
                &&
                WriteCharacterStatsFile(stats, SaveDataPath + "/" + SaveSlot + "/", "SavedStats"))
            {
                return true;
            }
            return false;
        }
        /*public bool WritePlayerFile(string FileName)
        {
            List<PlayerCharacter> list = new List<PlayerCharacter>();

            PlayerCharacter c = new PlayerCharacter() { CharacterID = "00" };
            c.Name = "Rain";
            c.CharacterStats = new CharacterStats(300, 50, 0, 5, 5, 5, 5, 5, 1, 1, 1, 1);
            c.CharacterStatus = new CharacterStatus(c.CharacterStats);

            list.Add(c);
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(DataFilesPath + FileName + ".json", json);

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not write player file!");
            }

            return false;
        }*/
        public bool WriteStringFile(List<string> list, string Path, string FileName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(Path + FileName + ".json", json);

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not write string file '" + FileName + "' !");
            }

            return false;
        }
        public List<string> ReadStringFile(string Path, string FileName)
        {
            List<string> list = new List<string>();
            try
            {
                string fileString = File.ReadAllText(Path + FileName + ".json");
                var result = JsonConvert.DeserializeObject<List<string>>(fileString);

                foreach (string data in result)
                {
                    list.Add(data);
                }

                return list;
            }
            catch
            {
                Debug.WriteLine("Could not read string file '" + FileName + "' !");
            }
            return null;
        }
        public bool WriteCharacterStatsFile(List<CharacterStats> list, string Path, string FileName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(Path + FileName + ".json", json);

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not write CharacterStats file '" + FileName + "' !");
            }
            return false;
        }
        public List<CharacterStats> ReadCharacterStatsFile(string Path, string FileName)
        {
            List<CharacterStats> list = new List<CharacterStats>();
            try
            {
                string fileString = File.ReadAllText(Path + FileName + ".json");
                var result = JsonConvert.DeserializeObject<List<CharacterStats>>(fileString);

                foreach (CharacterStats data in result)
                {
                    list.Add(data);
                }

                return list;
            }
            catch
            {
                Debug.WriteLine("Could not read CharacterStats file '" + FileName + "' !");
            }
            return null;
        }
        /*public bool WriteFile(string FileName)
        {
            List<EnemyCharacter> list = new List<EnemyCharacter>();

            EnemyCharacter c = new EnemyCharacter() { CharacterID = "00" };
            c.Name = "Rain";
            c.CharacterStats = new CharacterStats(300, 50, 0, 5, 5, 5, 5, 5, 1, 1, 1, 1);
            c.CharacterAnimationList.Add(new CharacterAnimation());
            c.BehaviorList.Add(new BasicAttackBehavior("Basic Attack"));

            list.Add(c);
            list.Add(c);
            try
            {
                string json = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                File.WriteAllText(DataFilesPath + "/EnemyFiles/00/" + FileName + ".json", json);

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not write player file!");
            }

            return false;
        }*/
        public List<EnemyCharacter> ReadEnemyFile(string Path, string FileName)
        {
            List<EnemyCharacter> list = new List<EnemyCharacter>();
            try
            {
                string fileString = File.ReadAllText(Path + FileName + ".json");
                var result = JsonConvert.DeserializeObject<List<EnemyCharacter>>(fileString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                foreach (EnemyCharacter data in result)
                {
                    list.Add(data);
                }

                return list;
            }
            catch
            {
                Debug.WriteLine("Could not read file '" + FileName + "' !");
            }
            return null;
        }
        public List<PlayerCharacter> ReadPlayerFile(string Path, string FileName)
        {
            List<PlayerCharacter> list = new List<PlayerCharacter>();
            try
            {
                string fileString = File.ReadAllText(Path + FileName + ".json");
                var result = JsonConvert.DeserializeObject<List<PlayerCharacter>>(fileString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                foreach (PlayerCharacter data in result)
                {
                    list.Add(data);
                }

                return list;
            }
            catch
            {
                Debug.WriteLine("Could not read Behavior file '" + FileName + "' !");
            }
            return null;
        }
        public bool WriteBehaviorFile(string FileName)
        {
            List<IAttackBehavior> list = new List<IAttackBehavior>();

            list.Add(new BasicAttackBehavior("Basic Attack"));
            list.Add(new BasicAttackBehavior("Strong Attack"));

            try
            {
                string json = JsonConvert.SerializeObject(list, Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });
                File.WriteAllText(DataFilesPath + FileName + ".json", json);

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not write player file!");
            }

            return false;
        }
        public List<IAttackBehavior> ReadBehaviorFile(string Path, string FileName)
        {
            List<IAttackBehavior> list = new List<IAttackBehavior>();
            try
            {
                string fileString = File.ReadAllText(Path + FileName + ".json");
                var result = JsonConvert.DeserializeObject<List<IAttackBehavior>>(fileString, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto
                });

                foreach (IAttackBehavior data in result)
                {
                    list.Add(data);
                }

                return list;
            }
            catch
            {
                Debug.WriteLine("Could not read Behavior file '" + FileName + "' !");
            }
            return null;
        }
    }
}
