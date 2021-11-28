using Game.Scripts.Resources;
using UnityEngine;

namespace Game.Scripts.Data
{
    [System.Serializable]
    public class GameData
    {
        public int highScore;

        public GameData(ResourceContainer resources)
        {
            highScore = resources.HighScoreCount;
        }
    }
}