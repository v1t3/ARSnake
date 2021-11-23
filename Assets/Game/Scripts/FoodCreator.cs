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
            return allFood.Any(food => food.selfPosition == newPosition);
        }

        public bool CheckFood(Vector2Int position)
        {
            return allFood.Any(food => food.selfPosition == position);
        }

        public Food GetFood(Vector2Int position)
        {
            return allFood.FirstOrDefault(food => food.selfPosition == position);
        }

        public void DestroyFood(Vector2Int position)
        {
            for (var i = 0; i < allFood.Count; i++)
            {
                if (allFood[i].selfPosition != position) continue;
                
                Destroy(allFood[i].gameObject);
                allFood.RemoveAt(i);
                break;
            }
        }

        public void DestroyAllFood()
        {
            for (var i = 0; i < allFood.Count; i++)
            {
                Destroy(allFood[i].gameObject);
                allFood.RemoveAt(i);
            }
        }
    }
}