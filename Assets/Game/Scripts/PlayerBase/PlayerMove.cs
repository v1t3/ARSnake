using System.Collections.Generic;
using Game.Scripts.Resources;
using UnityEngine;

namespace Game.Scripts.PlayerBase
{
    public class PlayerMove : MonoBehaviour
    {
        private GameManager _gameManager;
        private FoodCreator _foodCreator;
        private Player _player;
        private ResourceContainer _resourceContainer;

        [SerializeField] private GameObject tailPrefab;
        [SerializeField] private float normalStepDelay = 0.2f;
        [SerializeField] private float fastStepDelay = 0.1f;
        private float _stepDelay;

        private Vector2Int _startDirection = Vector2Int.up;
        private Vector2Int _currentDirection;

        private List<Transform> _parts = new List<Transform>();
        [SerializeField] private List<Transform> startParts = new List<Transform>();
        private List<Vector2Int> _positions = new List<Vector2Int>();
        private List<Vector2Int> _startPositions = new List<Vector2Int>();

        private float _timer;

        public List<Vector2Int> Positions => _positions;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _foodCreator = FindObjectOfType<FoodCreator>();
            _player = FindObjectOfType<Player>();
            _resourceContainer = FindObjectOfType<ResourceContainer>();
        }

        private void Start()
        {
            SetStartData();
            SetNormalStep();
        }

        private void Update()
        {
            if (!_gameManager.IsGameActive) return;
            _timer += Time.deltaTime;

            if (Input.GetKeyDown((KeyCode.W)))
            {
                if (_currentDirection != Vector2Int.down)
                {
                    _currentDirection = Vector2Int.up;
                }
            }
            else if (Input.GetKeyDown((KeyCode.S)))
            {
                if (_currentDirection != Vector2Int.up)
                {
                    _currentDirection = Vector2Int.down;
                }
            }
            else if (Input.GetKeyDown((KeyCode.A)))
            {
                if (_currentDirection != Vector2Int.right)
                {
                    _currentDirection = Vector2Int.left;
                }
            }
            else if (Input.GetKeyDown((KeyCode.D)))
            {
                if (_currentDirection != Vector2Int.left)
                {
                    _currentDirection = Vector2Int.right;
                }
            }

            // if (Input.GetKey(KeyCode.LeftShift))
            // {
            //     SetFastStep();
            // }
            // else
            // {
            //     SetNormalStep();
            // }

            if (_timer > _stepDelay)
            {
                _timer = 0;
                UpdatePositions();
            }
        }

        private void UpdatePositions()
        {
            Vector2Int firstPos = _positions[0];
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

            _positions.Insert(0, firstPos);
            _positions.RemoveAt(_positions.Count - 1);

            //update parts positions
            for (int i = 0, len = _positions.Count; i < len; i++)
            {
                _parts[i].localPosition = new Vector3(_positions[i].x, _positions[i].y, 0);
            }

            if (CheckSelfIntersection(firstPos))
            {
                _player.HitTail();
            }

            CheckFood(firstPos);
        }

        /**
         * Used in Joystick
         */
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

        /**
         * Used in Joystick
         */
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
            var newPos = _positions[_positions.Count - 1];
            var newTailPart = Instantiate(tailPrefab, gameObject.transform);
            newTailPart.transform.localPosition = new Vector3(newPos.x, newPos.y, 0);

            _parts.Add(newTailPart.transform);
            _positions.Add(_positions[_positions.Count - 1]);
        }

        private bool CheckSelfIntersection(Vector2Int newPosition)
        {
            for (int i = 1; i < _positions.Count; i++)
            {
                if (_positions[i] == newPosition)
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

                Food food = _foodCreator.GetFood(position);

                if (food)
                {
                    _resourceContainer.UpdatePoints(food.Points);
                }

                _foodCreator.DestroyFood(position);
                _foodCreator.Create();
            }
        }

        private void SetStartData()
        {
            foreach (var part in startParts)
            {
                var partPos = part.transform.localPosition;
                _positions.Add(new Vector2Int((int)partPos.x, (int)partPos.y));
            }

            _parts = new List<Transform>(startParts);
            _startPositions = new List<Vector2Int>(_positions);
            _currentDirection = _startDirection;
        }

        /**
         * Using in SpeedUp Button
         */
        public void SetNormalStep()
        {
            _stepDelay = normalStepDelay;
        }

        /**
         * Using in SpeedUp Button
         */
        public void SetFastStep()
        {
            _stepDelay = fastStepDelay;
        }

        public void ResetMove()
        {
            foreach (var part in _parts)
            {
                if (startParts.Contains(part)) continue;

                Destroy(part.gameObject);
            }
            
            _parts = new List<Transform>(startParts);
            _positions = new List<Vector2Int>(_startPositions);
            _currentDirection = _startDirection;
        }
    }
}