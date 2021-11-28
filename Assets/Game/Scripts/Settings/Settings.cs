using UnityEngine;

namespace Game.Scripts.Settings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject grid;
        
        private bool _isGridEnabled;

        public void ToggleGrid(bool value)
        {
            _isGridEnabled = value;
            grid.SetActive(_isGridEnabled);
        }
    }
}