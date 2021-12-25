using Game.Scripts.Settings;

namespace Game.Scripts.Data
{
    [System.Serializable]
    public class GameSettingsData
    {
        private bool _isGridEnabled;
        public bool IsGridEnabled => _isGridEnabled;
        
        private int _inputTypeId;
        public int InputTypeId => _inputTypeId;

        private int _menuTypeId;
        public int MenuTypeId => _menuTypeId;

        public GameSettingsData(GameSettings gameSettings)
        {
            _isGridEnabled = gameSettings.IsGridEnabled;
            _inputTypeId = gameSettings.InputTypeId;
            _menuTypeId = gameSettings.MenuTypeId;
        }
    }
}