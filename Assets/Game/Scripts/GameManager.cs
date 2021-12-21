using Game.Scripts.Resources;
using Game.Scripts.Settings;
using Game.Scripts.Units;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private FoodCreator _foodCreator;
        [SerializeField] private MenuManager menuManager;
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
            menuManager.UpdatePointCountText(_resourceContainer.PointsCount);
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
            playerMove.MenuManager = menuManager;
            _placeManager.MenuManager = menuManager;
        }

        private void SetFieldParameters()
        {
            fieldHeight = (int)background.transform.localScale.x;
            fieldWidth = (int)background.transform.localScale.y;
        }

        public void PrepareGame()
        {
            menuManager.ShowPrepareMenu();

            _placeManager.ResetPosition();
            _resourceContainer.Reset();
            menuManager.UpdatePointCountText(_resourceContainer.PointsCount);
            player.gameObject.SetActive(false);

            IsGameActive = false;
            PrepareMode = true;
        }

        public void CancelPrepareGame()
        {
            menuManager.HidePrepareMenu();

            PrepareMode = false;

            _placeManager.DisablePlacement();
        }

        public void StartGame()
        {
            menuManager.ShowStartMenu();

            player.gameObject.SetActive(true);

            PrepareMode = false;
            IsGameActive = true;
            _markerController.DisableMarker();
            _resourceContainer.UpdateHighScore();
            menuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
        }

        public void RestartGame()
        {
            menuManager.RestartMenus();

            ResetGameParams();
            PrepareGame();
        }

        public void ShowSettings()
        {
            _gameSettings.LoadGameSettings();
            menuManager.ShowSettingsMenu();
        }

        public void HideSettings()
        {
            _gameSettings.SaveGameSettings();
            menuManager.HideSettingsMenu();
        }

        public void PauseGame()
        {
            IsGameActive = false;
            menuManager.ShowPauseMenu();
        }

        public void UnpauseGame()
        {
            IsGameActive = true;
            menuManager.HidePauseMenu();
        }

        public void ReturnToMainMenu()
        {
            menuManager.ReturnToMainMenu();

            ResetGameParams();
        }

        public void GameOver()
        {
            Debug.Log("GameOver");

            IsGameActive = false;
            menuManager.ShowGameOverMenu();
            menuManager.UpdateScoreText(_resourceContainer.PointsCount);
            _resourceContainer.UpdateHighScore();
            menuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
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