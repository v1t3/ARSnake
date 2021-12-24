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

    public class GameManager : MonoBehaviour
    {
        private FoodCreator _foodCreator;
        [SerializeField] private MenuManager currentMenuManager;
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

        [SerializeField] private InputType currentInputType;

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
            SetManagers();
            SetFieldParameters();
            _placeManager.DisablePlacement();
            currentMenuManager.UpdatePointCountText(_resourceContainer.PointsCount);
        }

        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Exit();
            }
        }

        private void SetManagers()
        {
            playerMove.MenuManager = currentMenuManager;
            _placeManager.MenuManager = currentMenuManager;
        }

        private void SetFieldParameters()
        {
            fieldHeight = (int)background.transform.localScale.x;
            fieldWidth = (int)background.transform.localScale.y;
        }

        public void PrepareGame()
        {
            currentMenuManager.ShowPrepareMenu();

            _placeManager.ResetPosition();
            _resourceContainer.Reset();
            currentMenuManager.UpdatePointCountText(_resourceContainer.PointsCount);
            currentMenuManager.DisableJoystick();
            player.gameObject.SetActive(false);

            IsGameActive = false;
            PrepareMode = true;
        }

        public void CancelPrepareGame()
        {
            currentMenuManager.HidePrepareMenu();

            PrepareMode = false;

            _placeManager.DisablePlacement();
        }

        public void StartGame()
        {
            currentMenuManager.ShowStartMenu();

            player.gameObject.SetActive(true);

            PrepareMode = false;
            IsGameActive = true;
            _markerController.DisableMarker();
            _resourceContainer.UpdateHighScore();
            
            currentMenuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
            currentMenuManager.EnableJoystick();
        }

        public void RestartGame()
        {
            currentMenuManager.RestartMenus();

            ResetGameParams();
            PrepareGame();
        }

        public void ShowSettings()
        {
            _gameSettings.LoadGameSettings();
            currentMenuManager.ShowSettingsMenu();
        }

        public void HideSettings()
        {
            _gameSettings.SaveGameSettings();
            currentMenuManager.HideSettingsMenu();
        }

        public void PauseGame()
        {
            IsGameActive = false;
            currentMenuManager.ShowPauseMenu();
        }

        public void UnpauseGame()
        {
            IsGameActive = true;
            currentMenuManager.HidePauseMenu();
        }

        public void ReturnToMainMenu()
        {
            currentMenuManager.ReturnToMainMenu();

            ResetGameParams();
        }

        public InputType GetInputType()
        {
            return currentInputType;
        }

        public void SetInputType(int inputTypeId)
        {
            currentInputType = (InputType)inputTypeId;

            currentMenuManager.SetInputType(currentInputType);
        }
        
        public bool IsTouch()
        {
            return currentInputType == InputType.Touch;
        }

        public void GameOver()
        {
            Debug.Log("GameOver");

            IsGameActive = false;
            currentMenuManager.ShowGameOverMenu();
            currentMenuManager.UpdateScoreText(_resourceContainer.PointsCount);
            _resourceContainer.UpdateHighScore();
            currentMenuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
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