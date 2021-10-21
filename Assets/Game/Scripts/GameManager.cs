using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private FoodCreator _foodCreator;
        private MenuManager _menuManager;

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

        public void StartPlay()
        {
            Debug.Log("StartPlay");
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

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void SetPrepareMode(bool value)
        {
            prepareMode = value;
        }
    }
}
