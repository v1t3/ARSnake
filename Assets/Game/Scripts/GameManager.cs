using Game.Scripts.Resources;
using Game.Scripts.Settings;
using Game.Scripts.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts
{
    public enum InputType
    {
        Buttons,
        Touch
    }

    public enum MenuType
    {
        Menu2D,
        Menu3D
    }

    public class GameManager : MonoBehaviour
    {
        private FoodCreator _foodCreator;
        private PlaceManager _placeManager;
        private ResourceContainer _resourceContainer;
        private MarkerController _markerController;
        private GameSettings _gameSettings;

        [SerializeField] private Snake player;
        [SerializeField] private SnakeMovement playerMove;

        [SerializeField] private GameObject background;

        [HideInInspector] public int fieldHeight;
        [HideInInspector] public int fieldWidth;

        private bool _prepareMode;
        public bool PrepareMode
        {
            get => _prepareMode;
            private set => _prepareMode = value;
        }

        private bool _isFieldPlaced;
        public bool IsFieldPlaced
        {
            get => _isFieldPlaced;
            set => _isFieldPlaced = value;
        }

        private bool _isGameActive;
        public bool IsGameActive
        {
            get => _isGameActive;
            set => _isGameActive = value;
        }

        private InputType _currentInputType;

        [SerializeField] private MenuManager menuManager2d;
        [SerializeField] private MenuManager menuManager3d;
        private MenuManager _currentMenuManager;
        private MenuType _currentMenuType;

        private void Awake()
        {
            _foodCreator = FindObjectOfType<FoodCreator>();
            _placeManager = FindObjectOfType<PlaceManager>();
            _resourceContainer = FindObjectOfType<ResourceContainer>();
            _markerController = FindObjectOfType<MarkerController>();
            _gameSettings = FindObjectOfType<GameSettings>();
        }

        private void Start()
        {
            SetFieldParameters();
            
            UpdateMenuManagerData();
        }

        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Exit();
            }
        }

        public void OnLoadSettings()
        {
            UpdateMenuManagerData();
        }

        public void OnSaveSettings()
        {
            UpdateMenuManagerData();
        }

        private void UpdateMenuManagerData()
        {
            SetMenuType((int)_currentMenuType);
            SetMenuManagers();
            _placeManager.DisablePlacement();
            _currentMenuManager.UpdatePointCountText(_resourceContainer.PointsCount);
        }

        private void SetMenuManagers()
        {
            playerMove.MenuManager = _currentMenuManager;
            _placeManager.MenuManager = _currentMenuManager;
        }

        private void SetFieldParameters()
        {
            fieldHeight = (int)background.transform.localScale.x;
            fieldWidth = (int)background.transform.localScale.y;
        }

        public void PrepareGame()
        {
            _currentMenuManager.ShowPrepareMenu();

            _placeManager.ResetPosition();
            _resourceContainer.Reset();
            _currentMenuManager.UpdatePointCountText(_resourceContainer.PointsCount);
            _currentMenuManager.DisableJoystick();
            player.gameObject.SetActive(false);

            IsGameActive = false;
            PrepareMode = true;
        }

        public void CancelPrepareGame()
        {
            _currentMenuManager.HidePrepareMenu();

            PrepareMode = false;

            _placeManager.DisablePlacement();
        }

        public void StartGame()
        {
            _currentMenuManager.ShowStartMenu();

            player.gameObject.SetActive(true);

            PrepareMode = false;
            IsGameActive = true;
            _markerController.DisableMarker();
            _resourceContainer.UpdateHighScore();
            
            _currentMenuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
            _currentMenuManager.EnableJoystick();
        }

        public void RestartGame()
        {
            _currentMenuManager.RestartMenus();

            ResetGameParams();
            PrepareGame();
        }

        public void ShowSettings()
        {
            _gameSettings.LoadGameSettings();
            _currentMenuManager.ShowSettingsMenu();
        }

        public void HideSettings()
        {
            _gameSettings.SaveGameSettings();
            _currentMenuManager.HideSettingsMenu();
        }

        public void PauseGame()
        {
            IsGameActive = false;
            _currentMenuManager.ShowPauseMenu();
        }

        public void UnpauseGame()
        {
            IsGameActive = true;
            _currentMenuManager.HidePauseMenu();
        }

        public void ReturnToMainMenu()
        {
            _currentMenuManager.ReturnToMainMenu();

            ResetGameParams();
        }

        public InputType GetInputType()
        {
            return _currentInputType;
        }

        public void SetInputType(int inputTypeId)
        {
            _currentInputType = (InputType)inputTypeId;

            _currentMenuManager.SetInputType(_currentInputType);
        }

        public MenuType GetMenuType()
        {
            return _currentMenuType;
        }

        public void SetMenuTypeFromUI(int menuTypeId)
        {
            SetMenuType(menuTypeId);
            _gameSettings.SaveGameSettings();
            ShowSettings();
        }

        public void SetMenuType(int menuTypeId)
        {
            _currentMenuType = (MenuType)menuTypeId;
            
            if (_currentMenuType == MenuType.Menu3D)
            {
                _currentMenuManager = menuManager3d;
                menuManager3d.EnableMenu();
                menuManager3d.SetMenuTypeDropdown(menuTypeId);
                menuManager2d.DisableMenu();
            }
            else
            {
                _currentMenuManager = menuManager2d;
                menuManager2d.EnableMenu();
                menuManager2d.SetMenuTypeDropdown(menuTypeId);
                menuManager3d.DisableMenu();
            }
        }
        
        public bool IsTouch()
        {
            return _currentInputType == InputType.Touch;
        }

        public void GameOver()
        {
            Debug.Log("GameOver");

            IsGameActive = false;
            _currentMenuManager.ShowGameOverMenu();
            _currentMenuManager.UpdateScoreText(_resourceContainer.PointsCount);
            _resourceContainer.UpdateHighScore();
            _currentMenuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
        }

        public void Exit()
        {
            Debug.Log("Exit");
            Application.Quit();
        }

        private void ResetGameParams()
        {
            playerMove.ResetMove();
            player.gameObject.SetActive(false);
            _foodCreator.DestroyAllFood();
            _placeManager.DisablePlacement();
        }
    }
}