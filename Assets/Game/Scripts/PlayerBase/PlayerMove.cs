using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.PlayerBase
{
    public class PlayerMove : MonoBehaviour
    {
        private GameManager _gameManager;
        private FoodCreator _foodCreator;
        private Player _player;

        [SerializeField] private GameObject fieldWrap;
        [SerializeField] private GameObject tailPrefab;
        [SerializeField] private float normalStepDelay = 0.2f;
        [SerializeField] private float fastStepDelay = 0.1f;
        private float _stepDelay;

        private Vector2Int _startDirection = Vector2Int.up;
        private Vector2Int _currentDirection;

        [SerializeField] private List<Transform> parts = new List<Transform>();
        // [HideInInspector]
        public List<Vector2Int> positions = new List<Vector2Int>();
        public List<Transform> _startParts = new List<Transform>();
        public List<Vector2Int> _startPositions = new List<Vector2Int>();

        private float _timer;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _foodCreator = FindObjectOfType<FoodCreator>();
            _player = FindObjectOfType<Player>();
        }

        private void Start()
        {
            foreach (var part in parts)
            {
                var partPos = part.transform.localPosition;
                positions.Add(new Vector2Int((int)partPos.x, (int)partPos.y));
            }

            _startParts = new List<Transform>(parts);
            _startPositions = new List<Vector2Int>(positions);
            _currentDirection = _startDirection;
            
            SetNormalStep();
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

            //update parts positions
            for (int i = 0, len = positions.Count; i < len; i++)
            {
                parts[i].localPosition = new Vector3(positions[i].x, positions[i].y, 0);
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
            var newPos = positions[positions.Count - 1];
            var newTailPart = Instantiate(tailPrefab, fieldWrap.transform);
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
                
                Food food = _foodCreator.GetFood(position);

                if (food)
                {
                    _player.UpdatePointCount(food.points);
                }
                
                _foodCreator.DestroyFood(position);
                _foodCreator.Create();
            }
        }

        public void SetNormalStep()
        {
            _stepDelay = normalStepDelay;
        }

        public void SetFastStep()
        {
            _stepDelay = fastStepDelay;
        }

        public void ResetMove()
        {
            foreach (var part in parts)
            {
                if (!_startParts.Contains(part))
                {
                    Destroy(part.gameObject);
                }
            }
            
            parts = new List<Transform>(_startParts);
            positions = new List<Vector2Int>(_startPositions);
            _currentDirection = _startDirection;
        }
    }
}