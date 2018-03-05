using MainDFF.Classes.Battle;
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
        private string DataFilesPath = AppDomain.CurrentDomain.BaseDirectory + "/DataFiles/";
        private string SaveDataPath = AppDomain.CurrentDomain.BaseDirectory + "/SaveData/";
        public int SaveSlot = 0;
        public FileHelper(DataFileLists dataFileLists)
        {
            dataFileLists.GetBasicFiles(
                ReadStringFile(DataFilesPath, "PlayerID"), 
                ReadStringFile(DataFilesPath, "PlayerClass"), 
                ReadStringFile(DataFilesPath, "PlayerName"), 
                ReadCharacterStatsFile(DataFilesPath, "PlayerBasicStats"), 
                ReadCharacterAnimationFile(DataFilesPath, "PlayerSprite"), 
                ReadStringFile(DataFilesPath, "ClassNames"));
            if (!Directory.Exists(SaveDataPath))
            {
                for (int i = 0; i < 3; i++)
                {
                    Directory.CreateDirectory(SaveDataPath + SaveSlot);
                }
            }
        }
        public void LoadData(DataFileLists dataFileLists)
        {
            dataFileLists.playerCurrentPartyIDList = ReadStringFile(SaveDataPath + SaveSlot, "PlayerPartyID");
            dataFileLists.playerCurrentStats = ReadCharacterStatsFile(SaveDataPath + SaveSlot, "PlayerStats");
        }
        public void SaveData(DataFileLists dataFileLists)
        {
            var partyID = dataFileLists.playerCurrentPartyIDList;
            var currentStats = dataFileLists.playerCurrentStats;

            WriteStringFile(partyID, SaveDataPath + SaveSlot, "PlayerPartyID");
            WriteCharacterStatsFile(currentStats, SaveDataPath + SaveSlot, "PlayerStats");
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
        public List<CharacterStats> WriteCharacterStatsFile(List<CharacterStats> list, string Path, string FileName)
        {
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

        public List<List<CharacterAnimation>> ReadCharacterAnimationFile(string Path, string FileName)
        {
            List<List<CharacterAnimation>> list = new List<List<CharacterAnimation>>();

            try
            {
                string fileString = File.ReadAllText(Path + FileName + ".json");
                var result = JsonConvert.DeserializeObject<List<List<CharacterAnimation>>>(fileString);

                foreach (List<CharacterAnimation> data in result)
                {
                    list.Add(data);
                }

                return list;
            }
            catch
            {
                Debug.WriteLine("Could not read CharacterAnimation file '" + FileName + "' !");
            }
            return null;
        }
    }
}
