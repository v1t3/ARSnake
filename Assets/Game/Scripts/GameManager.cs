using Game.Scripts.Resources;
using Game.Scripts.Settings;
using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private FoodCreator _foodCreator;
        private MenuManager _menuManager;
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
        private bool _isFieldPlaced;
        private bool _isGameActive;

        public bool PrepareMode
        {
            get => _prepareMode;
            private set => _prepareMode = value;
        }

        public bool IsFieldPlaced
        {
            get => _isFieldPlaced;
            set => _isFieldPlaced = value;
        }
        
        public bool IsGameActive
        {
            get => _isGameActive;
            set => _isGameActive = value;
        }

        private void Awake()
        {
            _foodCreator = FindObjectOfType<FoodCreator>();
            _menuManager = FindObjectOfType<MenuManager>();
            _placeManager = FindObjectOfType<PlaceManager>();
            _resourceContainer = FindObjectOfType<ResourceContainer>();
            _markerController = FindObjectOfType<MarkerController>();
            _gameSettings = FindObjectOfType<GameSettings>();
        }

        private void Start()
        {
            SetFieldParameters();
            _placeManager.DisablePlacement();
            _menuManager.UpdatePointCountText(_resourceContainer.PointsCount);
        }
        
        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Exit();
            }
        }

        private void SetFieldParameters()
        {
            fieldHeight = (int)background.transform.localScale.x;
            fieldWidth = (int)background.transform.localScale.y;
        }

        public void PrepareGame()
        {
            _menuManager.startMenuPanel.SetActive(false);
            _menuManager.prepareMenuPanel.SetActive(true);
            _menuManager.prepareBottomMenu.SetActive(false);
            
            _placeManager.ResetPosition();
            _resourceContainer.Reset();
            _menuManager.UpdatePointCountText(_resourceContainer.PointsCount);
            player.gameObject.SetActive(false);
            
            IsGameActive = false;
            PrepareMode = true;
        }

        public void CancelPrepareGame()
        {
            _menuManager.startMenuPanel.SetActive(true);
            _menuManager.prepareMenuPanel.SetActive(false);
            
            PrepareMode = false;
            
            _placeManager.DisablePlacement();
        }

        public void StartGame()
        {
            _menuManager.prepareMenuPanel.SetActive(false);
            _menuManager.playMenuPanel.SetActive(true);
            
            player.gameObject.SetActive(true);
            
            PrepareMode = false;
            IsGameActive = true;
            _markerController.DisableMarker();
            _resourceContainer.UpdateHighScore();
            _menuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
        }

        public void RestartGame()
        {
            _menuManager.pauseMenuPanel.SetActive(false);
            _menuManager.playMenuPanel.SetActive(false);
            _menuManager.gameOverMenuPanel.SetActive(false);

            ResetGameParams();
            PrepareGame();
        }

        public void ShowSettings()
        {
            _gameSettings.LoadGameSettings();
            _menuManager.settingsMenuPanel.SetActive(true);
            _menuManager.startMenuPanel.SetActive(false);
        }

        public void HideSettings()
        {
            _gameSettings.SaveGameSettings();
            _menuManager.settingsMenuPanel.SetActive(false);
            _menuManager.startMenuPanel.SetActive(true);
        }

        public void PauseGame()
        {
            IsGameActive = false;
            _menuManager.pauseMenuPanel.SetActive(true);
        }

        public void UnpauseGame()
        {
            IsGameActive = true;
            _menuManager.pauseMenuPanel.SetActive(false);
        }

        public void ReturnToMainMenu()
        {
            _menuManager.playMenuPanel.SetActive(false);
            _menuManager.pauseMenuPanel.SetActive(false);
            _menuManager.gameOverMenuPanel.SetActive(false);
            _menuManager.startMenuPanel.SetActive(true);

            ResetGameParams();
        }

        public void GameOver()
        {
            Debug.Log("GameOver");

            IsGameActive = false;
            _menuManager.playMenuPanel.SetActive(false);
            _menuManager.gameOverMenuPanel.SetActive(true);
            
            _menuManager.UpdateScoreText(_resourceContainer.PointsCount);
            _resourceContainer.UpdateHighScore();
            _menuManager.UpdateHighScoreText(_resourceContainer.HighScoreCount);
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