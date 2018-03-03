using MainDFF.Classes.Battle;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDFF.Classes.FileHelper
{
    class FileHelper
    {
        public bool ReadPlayerFile(List<ACharacter> list, string FileName)
        {
            try
            {
                string fileString = File.ReadAllText(FileName);
                var result = JsonConvert.DeserializeObject<List<ACharacter>>(fileString);

                foreach (ACharacter data in result)
                {
                    list.Add(data);
                }

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not read player file!");
            }
            return false;
        }

        public bool WritePlayerFile(List<ACharacter> list, string FileName)
        {
            try
            {
                string json = JsonConvert.SerializeObject(list);
                File.WriteAllText(FileName, json);

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not write player file!");
            }

            return false;
        }

        public bool ReadSpriteFile(List<int> list, string FileName)
        {
            try
            {
                string fileString = File.ReadAllText(FileName);
                var result = JsonConvert.DeserializeObject<List<int>>(fileString);

                foreach (int data in result)
                {
                    list.Add(data);
                }

                return true;
            }
            catch
            {
                Debug.WriteLine("Could not read sprite file!");
            }
            return false;
        }
    }
}
