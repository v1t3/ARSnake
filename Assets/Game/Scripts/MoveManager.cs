using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class MoveManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private FoodCreator _foodCreator;

        [SerializeField] private GameObject parent;
        [SerializeField] private GameObject tailPrefab;
    
        private Vector2Int _currentDirection;

        [SerializeField] private List<Transform> parts = new List<Transform>();
    
        [HideInInspector]
        public List<Vector2Int> positions = new List<Vector2Int>();

        private float _timer;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _foodCreator = FindObjectOfType<FoodCreator>();
        }

        private void Start()
        {
            foreach (var part in parts)
            {
                var partPos = part.transform.localPosition;
                positions.Add(new Vector2Int((int)partPos.x, (int)partPos.y));
            }

            _currentDirection = Vector2Int.up;
        }

        private void Update()
        {
            if (!_gameManager.isGameActive) return;
            
            _timer += Time.deltaTime;

            if (Input.GetKeyDown((KeyCode.W)))
            {
                if (_currentDirection != Vector2Int.down)
                {
                    _currentDirection = Vector2Int.up;
                }
            }

            if (Input.GetKeyDown((KeyCode.S)))
            {
                if (_currentDirection != Vector2Int.up)
                {
                    _currentDirection = Vector2Int.down;
                }
            }

            if (Input.GetKeyDown((KeyCode.A)))
            {
                if (_currentDirection != Vector2Int.right)
                {
                    _currentDirection = Vector2Int.left;
                }
            }

            if (Input.GetKeyDown((KeyCode.D)))
            {
                if (_currentDirection != Vector2Int.left)
                {
                    _currentDirection = Vector2Int.right;
                }
            }

            if (_timer > 0.25f)
            {
                _timer = 0;
                Vector2Int firstPos = positions[0];
                firstPos += _currentDirection;

                if (firstPos.x >= _gameManager.fieldHeight)
                {
                    firstPos.x = 0;
                }
                else if (firstPos.x < 0)
                {
                    firstPos.x = _gameManager.fieldHeight - 1;
                }

                if (firstPos.y >= _gameManager.fieldWidth)
                {
                    firstPos.y = 0;
                }
                else if (firstPos.y < 0)
                {
                    firstPos.y = _gameManager.fieldWidth - 1;
                }

                positions.Insert(0, firstPos);
                positions.RemoveAt(positions.Count - 1);

                for (int i = 0, len = positions.Count; i < len; i++)
                {
                    parts[i].localPosition = new Vector3(positions[i].x, positions[i].y, 0);
                }

                if (CheckSelfIntersection(firstPos))
                {
                    Debug.Log("Lose");
                    _gameManager.isGameActive = false;
                }

                CheckFood(firstPos);
            }
        }

        public void SetDirectionX(int direction)
        {
            if (direction > 0)
            {
                if (_currentDirection != Vector2Int.left)
                {
                    _currentDirection = Vector2Int.right;
                }
            }
            else
            {
                if (_currentDirection != Vector2Int.right)
                {
                    _currentDirection = Vector2Int.left;
                }
            }
        }

        public void SetDirectionY(int direction)
        {
            if (direction > 0)
            {
                if (_currentDirection != Vector2Int.down)
                {
                    _currentDirection = Vector2Int.up;
                }
            }
            else
            {
                if (_currentDirection != Vector2Int.up)
                {
                    _currentDirection = Vector2Int.down;
                }
            }
        }

        private void AddTail()
        {
            var newPos = positions[positions.Count - 1];
            var newTailPart = Instantiate(tailPrefab, parent.transform);
            newTailPart.transform.localPosition = new Vector3(newPos.x, newPos.y, 0);

            parts.Add(newTailPart.transform);
            positions.Add(positions[positions.Count - 1]);
        }

        private bool CheckSelfIntersection(Vector2Int newPosition)
        {
            for (int i = 1; i < positions.Count; i++)
            {
                if (positions[i] == newPosition)
                {
                    return true;
                }
            }

            return false;
        }

        private void CheckFood(Vector2Int position)
        {
            if (_foodCreator.CheckFood(position))
            {
                AddTail();
            }
        }
    }
}