using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        private FoodCreator _foodCreator;

        [SerializeField] 
        private GameObject background;
        
        [HideInInspector]
        public bool isGameActive;

        [HideInInspector]
        public int fieldHeight;
        [HideInInspector]
        public int fieldWidth;

        private void Start()
        {
            _foodCreator = FindObjectOfType<FoodCreator>();
            
            fieldHeight = (int)background.transform.localScale.x;
            fieldWidth = (int)background.transform.localScale.y;
        }
        
        private void Update()
        {
            if (Input.GetKey("escape"))
            {
                Exit();
            }
        }

        public void StartGame()
        {
            isGameActive = true;
            _foodCreator.Create();
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
