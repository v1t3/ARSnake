using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Resources
{
    public class ResourceContainer : MonoBehaviour
    {
        [SerializeField] private Text pointsCountText;

        private int _pointsCount;
        
        public int PointsCount
        {
            get => _pointsCount;
            set
            {
                _pointsCount = value;
                UpdatePointCountText();
                
            }
        }

        private void Start()
        {
            UpdatePointCountText();
        }

        public void UpdatePoints(int value)
        {
            PointsCount += value;
        }

        private void UpdatePointCountText()
        {
            pointsCountText.text = _pointsCount.ToString();
        }

        public void Reset()
        {
            PointsCount = 0;
        }
    }
}