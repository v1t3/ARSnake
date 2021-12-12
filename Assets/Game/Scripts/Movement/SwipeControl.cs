using System;
using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Movement
{
    public enum SwipeState
    {
        Default,
        Tap,
        Dragging,
        LongTap
    }
    
    public enum SwipeDirection
    {
        None,
        Up,
        Down,
        Left,
        Right
    }

    public class SwipeControl : MonoBehaviour, IControl
    {
        private GameManager _gameManager;

        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform gameFieldTransform;

        [SerializeField] private SnakeMovement snakeMovement;

        [SerializeField] private float swipeDeadZone = 100;
        [SerializeField] private float swipeTapMinTime = 0.2f;

        private SwipeDirection _currentDirection = SwipeDirection.None;
        private SwipeState _swipeState = SwipeState.Default;

        private Vector2 _startTouch, _swipeDelta;

        private float _tapTimer;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (!_gameManager.IsGameActive) return;

            CheckSwipe();
        }

        private void CheckSwipe()
        {
            _currentDirection = SwipeDirection.None;

            #region Editor mouse swipes

            if (Input.GetMouseButtonDown(0))
            {
                _swipeState = SwipeState.Dragging;
                _startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }

            #endregion

            #region Mobile swipe

            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    _swipeState = SwipeState.Dragging;
                    _startTouch = Input.touches[0].position;
                }
                else if (
                    Input.touches[0].phase == TouchPhase.Ended ||
                    Input.touches[0].phase == TouchPhase.Canceled
                )
                {
                    ResetSwipe();
                }
            }

            #endregion

            _swipeDelta = Vector2.zero;

            // if (_swipeState == SwipeState.Tap)
            // {
            // }
            
            if (_swipeState == SwipeState.Dragging)
            {
                if (Input.touches.Length > 0)
                {
                    _swipeDelta = Input.touches[0].position - _startTouch;
                    
                }
                else if (Input.GetMouseButton(0))
                {
                    _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
                }
                
                Debug.Log("delta " + _swipeDelta.magnitude);
            }
                
            if (_swipeDelta.magnitude > swipeDeadZone)
            {
                float x = _swipeDelta.x;
                float y = _swipeDelta.y;

                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    if (x < 0)
                    {
                        _currentDirection = SwipeDirection.Left;
                    }
                    else
                    {
                        _currentDirection = SwipeDirection.Right;
                    }
                }
                else
                {
                    if (y < 0)
                    {
                        _currentDirection = SwipeDirection.Down;
                    }
                    else
                    {
                        _currentDirection = SwipeDirection.Up;
                    }
                }

                CheckSwipeDirection();
                ResetSwipe();
            }
        }

        private void CheckSwipeDirection()
        {
            if (_currentDirection == SwipeDirection.Up)
            {
                MoveUp();
            }
            else if (_currentDirection == SwipeDirection.Down)
            {
                MoveDown();
            }
            else if (_currentDirection == SwipeDirection.Left)
            {
                MoveLeft();
            }
            else if (_currentDirection == SwipeDirection.Right)
            {
                MoveRight();
            }
        }

        private void ResetSwipe()
        {
            _startTouch = _swipeDelta = Vector2.zero;
            _swipeState = SwipeState.Default;
        }

        public void MoveUp()
        {
            var angle = GetAngleToTarget();

            if (angle > -45 && angle < 45)
            {
                snakeMovement.MoveUp();
            }
            else if (angle >= 45 && angle < 105)
            {
                snakeMovement.MoveRight();
            }
            else if (angle <= -45 && angle > -105)
            {
                snakeMovement.MoveLeft();
            }
            else
            {
                snakeMovement.MoveDown();
            }
        }

        public void MoveDown()
        {
            var angle = GetAngleToTarget();

            if (angle > -45 && angle < 45)
            {
                snakeMovement.MoveDown();
            }
            else if (angle >= 45 && angle < 105)
            {
                snakeMovement.MoveLeft();
            }
            else if (angle <= -45 && angle > -105)
            {
                snakeMovement.MoveRight();
            }
            else
            {
                snakeMovement.MoveUp();
            }
        }

        public void MoveLeft()
        {
            var angle = GetAngleToTarget();

            if (angle > -45 && angle < 45)
            {
                snakeMovement.MoveLeft();
            }
            else if (angle >= 45 && angle < 105)
            {
                snakeMovement.MoveUp();
            }
            else if (angle <= -45 && angle > -105)
            {
                snakeMovement.MoveDown();
            }
            else
            {
                snakeMovement.MoveRight();
            }
        }

        public void MoveRight()
        {
            var angle = GetAngleToTarget();

            if (angle > -45 && angle < 45)
            {
                snakeMovement.MoveRight();
            }
            else if (angle >= 45 && angle < 105)
            {
                snakeMovement.MoveDown();
            }
            else if (angle <= -45 && angle > -105)
            {
                snakeMovement.MoveUp();
            }
            else
            {
                snakeMovement.MoveLeft();
            }
        }

        public void SetNormalSpeed()
        {
            snakeMovement.SetNormalStep();
        }

        public void SetFastSpeed()
        {
            snakeMovement.SetFastStep();
        }

        private float GetAngleToTarget()
        {
            Vector3 toTarget = gameFieldTransform.forward;
            toTarget.y = 0;
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;

            var angle = Vector3.Angle(toTarget, cameraForward);
            // Debug.Log("angle " + angle);

            Vector3 cross = Vector3.Cross(toTarget, cameraForward);
            // Debug.Log("cross " + cross);

            if (cross.y < 0)
            {
                angle = -angle;
            }

            // Debug.Log("angle " + angle);

            return angle;
        }
    }
}