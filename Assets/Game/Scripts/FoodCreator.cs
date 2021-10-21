using System.Collections.Generic;
using System.Linq;
using Game.Scripts.PlayerBase;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class FoodCreator : MonoBehaviour
    {
        private GameManager _gameManager;
        private PlayerMove _playerMove;

        [SerializeField] private GameObject parent;

        [SerializeField] private List<GameObject> foodPrefabs = new List<GameObject>();

        [SerializeField]
        private List<Food> allFood = new List<Food>();

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _playerMove = FindObjectOfType<PlayerMove>();
        }

        public void Create()
        {
            Vector2Int randomPos = GeneratePosition();

            while (CheckPlace(randomPos))
            {
                randomPos = GeneratePosition();
            }

            GameObject foodPrefab = foodPrefabs[Random.Range(0, foodPrefabs.Count)];
            GameObject newFood = Instantiate(foodPrefab, parent.transform);
            newFood.transform.localPosition = new Vector3(randomPos.x, randomPos.y, 0);
            newFood.GetComponent<Food>().selfPosition = randomPos;

            allFood.Add(newFood.GetComponent<Food>());
        }

        private Vector2Int GeneratePosition()
        {
            return new Vector2Int(
                Random.Range(0, _gameManager.fieldHeight),
                Random.Range(0, _gameManager.fieldWidth)
            );
        }

        private bool CheckPlace(Vector2Int newPosition)
        {
            //Check if snake
            if (_playerMove.positions.Any(position => position == newPosition))
            {
                return true;
            }

            //Check if food
            foreach (var food in allFood)
            {
                var foodPos = food.transform.position;
                var position = new Vector2Int((int)foodPos.x, (int)foodPos.y);

                if (position == newPosition)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CheckFood(Vector2Int position)
        {
            for (var i = 0; i < allFood.Count; i++)
            {
                if (allFood[i].selfPosition != position)
                {
                    continue;
                }
                
                Destroy(allFood[i].gameObject);
                allFood.RemoveAt(i);
                
                Create();

                return true;
            }

            return false;
        }
    }
}