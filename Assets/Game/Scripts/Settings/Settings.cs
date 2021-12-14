using Game.Scripts.Movement;
using UnityEngine;

namespace Game.Scripts.Settings
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject grid;
        
        [SerializeField] private Control[] inputTypes;
        
        private bool _isGridEnabled;

        private void Awake()
        {
            SetInputType(0);
        }

        public void ToggleGrid(bool value)
        {
            _isGridEnabled = value;
            grid.SetActive(_isGridEnabled);
        }

        public void SetInputType(int inputTypeId)
        {
            foreach (var inputType in inputTypes)
            {
                inputType.gameObject.SetActive(false);
            }
            
            inputTypes[inputTypeId].gameObject.SetActive(true);
        }
    }
}