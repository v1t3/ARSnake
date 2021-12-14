using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game.Scripts.Data;
using Game.Scripts.Movement;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Settings
{
    public class GameSettings : MonoBehaviour
    {
        [SerializeField] private Toggle gridToggle;
        [SerializeField] private GameObject grid;

        [SerializeField] private Dropdown inputTypeDropdown;
        [SerializeField] private Control[] inputTypes;
        
        private bool _isGridEnabled;
        public bool IsGridEnabled => _isGridEnabled;

        private int _inputTypeId;
        public int InputTypeId => _inputTypeId;

        private void Start()
        {
            GetIsGridEnabled();
            GetInputType();
            LoadGameSettings();
        }

        public void GetIsGridEnabled()
        {
            _isGridEnabled = gridToggle.isOn;
        }

        public void SetIsGridEnabled(bool value)
        {
            _isGridEnabled = value;
            grid.SetActive(_isGridEnabled);
        }

        public void GetInputType()
        {
            _inputTypeId = inputTypeDropdown.value;
        }

        public void SetInputType(int inputTypeId)
        {
            foreach (var inputType in inputTypes)
            {
                inputType.gameObject.SetActive(false);
            }
            
            inputTypes[inputTypeId].gameObject.SetActive(true);
        }

        public void LoadGameSettings()
        {
            var gameSettings = LoadData();

            if (null == gameSettings) return;
            
            _isGridEnabled = gameSettings.IsGridEnabled;
            _inputTypeId = gameSettings.InputTypeId;

            inputTypeDropdown.value = _inputTypeId;
            gridToggle.isOn = _isGridEnabled;
        }

        public void SaveGameSettings()
        {
            GetIsGridEnabled();
            GetInputType();
            SaveData();
        }

        private void SaveData()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + "/gameSettings.bin";

            FileStream stream = new FileStream(path, FileMode.Create);
            GameSettingsData data = new GameSettingsData(this);

            formatter.Serialize(stream, data);
            stream.Close();
        }

        private GameSettingsData LoadData()
        {
            string path = Application.persistentDataPath + "/gameSettings.bin";

            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                GameSettingsData data = formatter.Deserialize(stream) as GameSettingsData;
                stream.Close();

                return data;
            }

            Debug.LogWarning("File not found in path: " + path);
            return null;
        }
    }
}