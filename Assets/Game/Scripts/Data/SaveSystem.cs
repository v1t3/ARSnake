using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game.Scripts.Resources;
using UnityEngine;

namespace Game.Scripts.Data
{
    public static class SaveSystem
    {
        public static void SaveGameData(ResourceContainer resourceContainer)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/gameData.bin";

            FileStream stream = new FileStream(path, FileMode.Create);
            GameData data = new GameData(resourceContainer);
            
            formatter.Serialize(stream, data);
            stream.Close();
        }

        public static GameData LoadGameData()
        {
            string path = Application.persistentDataPath + "/gameData.bin";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                GameData data = formatter.Deserialize(stream) as GameData;
                stream.Close();

                return data;
            }

            Debug.LogError("File not found in path: " + path);
            return null;
        }
    }
}