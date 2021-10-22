using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.PlayerBase
{
    public class Player : MonoBehaviour
    {
        private GameManager _gameManager;

        [SerializeField] private Text pointsCountText;

        private int _pointsCount;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            UpdatePointCountText();
        }

        public void HitTail()
        {
            _gameManager.GameOver();
        }

        public void UpdatePointCount(int value)
        {
            _pointsCount += value;
            UpdatePointCountText();
        }

        private void UpdatePointCountText()
        {
            pointsCountText.text = _pointsCount.ToString(CultureInfo.InvariantCulture);
        }
    }
}