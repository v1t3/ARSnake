using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Game.Scripts.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Settings
{
    public class GameSettings : MonoBehaviour
    {
        private GameManager _gameManager;
        
        [SerializeField] private Toggle gridToggle;
        [SerializeField] private GameObject grid;
        
        private bool _isGridEnabled;
        public bool IsGridEnabled => _isGridEnabled;

        private int _inputTypeId;
        public int InputTypeId => _inputTypeId;

        private int _menuTypeId;
        public int MenuTypeId => _menuTypeId;
        
        // private bool _isARPlaneShowEnabled;
        // public bool IsARPlaneShowEnabled
        // {
        //     get => _isARPlaneShowEnabled;
        //     set => _isARPlaneShowEnabled = value;
        // }

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Start()
        {
            GetIsGridEnabled();
            GetInputType();
            GetMenuType();
            LoadGameSettings();
        }

        private void GetIsGridEnabled()
        {
            _isGridEnabled = gridToggle.isOn;
        }

        public void SetIsGridEnabled(bool value)
        {
            _isGridEnabled = value;
            grid.SetActive(_isGridEnabled);
        }

        // private void GetIsARPlaneShowEnabled()
        // {
        //     _isGridEnabled = _gameManager.GetIsARPlaneShowEnabled();
        // }

        private void GetInputType()
        {
            _inputTypeId = (int)_gameManager.GetInputType();
        }

        private void GetMenuType()
        {
            _menuTypeId = (int)_gameManager.GetMenuType();
        }

        public void LoadGameSettings()
        {
            var gameSettings = LoadData();

            if (null == gameSettings) return;
            
            _isGridEnabled = gameSettings.IsGridEnabled;
            _inputTypeId = gameSettings.InputTypeId;
            _menuTypeId = gameSettings.MenuTypeId;

            _gameManager.SetInputType(_inputTypeId);
            _gameManager.SetMenuType(_menuTypeId);
            gridToggle.isOn = _isGridEnabled;

            _gameManager.OnLoadSettings();
        }

        public void SaveGameSettings()
        {
            GetIsGridEnabled();
            GetInputType();
            GetMenuType();
            SaveData();

            _gameManager.OnSaveSettings();
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