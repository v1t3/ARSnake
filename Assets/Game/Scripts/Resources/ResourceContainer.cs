using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Resources
{
    public class ResourceContainer : MonoBehaviour
    {
        private int _pointsCount;
        private int _scoreCount;
        private int _highScoreCount;

        public int PointsCount
        {
            get => _pointsCount;
            set { _pointsCount = value; }
        }

        public int HighScoreCount
        {
            get => _highScoreCount;
            set => _highScoreCount = value;
        }

        public void UpdatePoints(int value)
        {
            PointsCount += value;
        }

        public void UpdateHighScore()
        {
            var gameData = LoadGameData();

            if (null == gameData || _pointsCount > gameData.highScore)
            {
                HighScoreCount = _pointsCount;
            }
            else
            {
                HighScoreCount = gameData.highScore;
            }

            SaveGameData();
        }

        public void Reset()
        {
            PointsCount = 0;
        }

        private void SaveGameData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/gameData.bin";

            FileStream stream = new FileStream(path, FileMode.Create);
            GameData data = new GameData(this);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        private GameData LoadGameData()
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