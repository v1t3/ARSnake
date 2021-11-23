using Game.Scripts.PlayerBase;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private FoodCreator _foodCreator;
        private MenuManager _menuManager;
        private PlaneManager _planeManager;
        private Player _player;
        private PlayerMove _playerMove;
        private PlaceManager _placeManager;

        [SerializeField] 
        private GameObject background;
        
        public bool prepareMode;
        public bool isFieldPlaced;
        // [HideInInspector]
        public bool isGameActive;

        [HideInInspector]
        public int fieldHeight;
        [HideInInspector]
        public int fieldWidth;

        private void Start()
        {
            _foodCreator = FindObjectOfType<FoodCreator>();
            _menuManager = FindObjectOfType<MenuManager>();
            _planeManager = FindObjectOfType<PlaneManager>();
            _player = FindObjectOfType<Player>();
            _playerMove = FindObjectOfType<PlayerMove>();
            _placeManager = FindObjectOfType<PlaceManager>();
            
            fieldHeight = (int)background.transform.localScale.x;
            fieldWidth = (int)background.transform.localScale.y;
        }
        
        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Exit();
                // TogglePausePlay();
            }
        }

        public void BeginPrepare()
        {
            Debug.Log("BeginPrepare");
            SetPrepareMode(true);
            isGameActive = false;
        }

        public void StartPlay()
        {
            Debug.Log("StartPlay");
            SetPrepareMode(false);
            _menuManager.ShowPlayMenu(true);
            isGameActive = true;
            _foodCreator.Create();
        }

        public void TogglePausePlay()
        {
            isGameActive = !isGameActive;
            _menuManager.TogglePausePlay(!isGameActive);
        }

        public void GameOver()
        {
            Debug.Log("GameOver");
            isGameActive = false;
            _menuManager.ShowGameOverMenu();
        }

        public void ReturnToMainMenu()
        {
            //todo
        }

        public void Reload()
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            isGameActive = false;
            _player.SetPointCount(0);
            _foodCreator.DestroyAllFood();
            _playerMove.ResetMove();
            StartPlay();
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void SetPrepareMode(bool value)
        {
            prepareMode = value;
            _planeManager.SetARPlane(value);
        }
    }
}
