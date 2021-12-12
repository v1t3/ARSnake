using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Movement
{
    public enum SwipeState
    {
        Default,
        Tap,
        Dragging
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

        [SerializeField] private float swipeDeadZone = 90;
        [SerializeField] private float tapMinTime = 0.5f;

        private SwipeDirection _currentDirection = SwipeDirection.None;
        private SwipeState _swipeState = SwipeState.Default;

        private Vector2 _startTouch, _swipeDelta;

        private bool _isPressed;
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
                _isPressed = true;
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
                    _isPressed = true;
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

            if (_isPressed)
            {
                if (Input.touches.Length > 0)
                {
                    _swipeDelta = Input.touches[0].position - _startTouch;
                }
                else if (Input.GetMouseButton(0))
                {
                    _swipeDelta = (Vector2)Input.mousePosition - _startTouch;
                }

                if (_swipeDelta.magnitude > 0)
                {
                    _swipeState = SwipeState.Dragging;
                }
                else
                {
                    _swipeState = SwipeState.Tap;
                }
            }

            switch (_swipeState)
            {
                case SwipeState.Tap:
                {
                    _tapTimer += Time.deltaTime;

                    if (_tapTimer > tapMinTime)
                    {
                        SetFastSpeed();
                    }

                    break;
                }
                case SwipeState.Dragging:
                {
                    ResetTap();

                    if (_swipeDelta.magnitude > swipeDeadZone)
                    {
                        float x = _swipeDelta.x;
                        float y = _swipeDelta.y;

                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            _currentDirection = x < 0 ? SwipeDirection.Left : SwipeDirection.Right;
                        }
                        else
                        {
                            _currentDirection = y < 0 ? SwipeDirection.Down : SwipeDirection.Up;
                        }

                        CheckSwipeDirection();
                        ResetSwipe();
                    }

                    break;
                }
            }
        }

        private void CheckSwipeDirection()
        {
            switch (_currentDirection)
            {
                case SwipeDirection.Up:
                    MoveUp();
                    break;
                case SwipeDirection.Down:
                    MoveDown();
                    break;
                case SwipeDirection.Left:
                    MoveLeft();
                    break;
                case SwipeDirection.Right:
                    MoveRight();
                    break;
            }
        }

        private void ResetSwipe()
        {
            _startTouch = _swipeDelta = Vector2.zero;
            _swipeState = SwipeState.Default;
            _isPressed = false;

            ResetTap();
        }

        private void ResetTap()
        {
            _tapTimer = 0;
            SetNormalSpeed();
        }

        public void MoveUp()
        {
            var direction = GetFieldDirection();

            switch (direction)
            {
                case FieldDirection.Forward:
                    snakeMovement.MoveUp();
                    break;
                case FieldDirection.Right:
                    snakeMovement.MoveRight();
                    break;
                case FieldDirection.Left:
                    snakeMovement.MoveLeft();
                    break;
                default:
                    snakeMovement.MoveDown();
                    break;
            }
        }

        public void MoveDown()
        {
            var direction = GetFieldDirection();

            switch (direction)
            {
                case FieldDirection.Forward:
                    snakeMovement.MoveDown();
                    break;
                case FieldDirection.Right:
                    snakeMovement.MoveLeft();
                    break;
                case FieldDirection.Left:
                    snakeMovement.MoveRight();
                    break;
                default:
                    snakeMovement.MoveUp();
                    break;
            }
        }

        public void MoveLeft()
        {
            var direction = GetFieldDirection();

            switch (direction)
            {
                case FieldDirection.Forward:
                    snakeMovement.MoveLeft();
                    break;
                case FieldDirection.Right:
                    snakeMovement.MoveUp();
                    break;
                case FieldDirection.Left:
                    snakeMovement.MoveDown();
                    break;
                default:
                    snakeMovement.MoveRight();
                    break;
            }
        }

        public void MoveRight()
        {
            var direction = GetFieldDirection();

            switch (direction)
            {
                case FieldDirection.Forward:
                    snakeMovement.MoveRight();
                    break;
                case FieldDirection.Right:
                    snakeMovement.MoveDown();
                    break;
                case FieldDirection.Left:
                    snakeMovement.MoveUp();
                    break;
                default:
                    snakeMovement.MoveLeft();
                    break;
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

        private FieldDirection GetFieldDirection()
        {
            var angle = GetAngleCameraToField();

            if (angle > -45 && angle < 45)
            {
                return FieldDirection.Forward;
            }

            if (angle >= 45 && angle < 105)
            {
                return FieldDirection.Right;
            }

            if (angle <= -45 && angle > -105)
            {
                return FieldDirection.Left;
            }

            return FieldDirection.Back;
        }

        private float GetAngleCameraToField()
        {
            Vector3 toTarget = gameFieldTransform.forward;
            toTarget.y = 0;
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;

            float angle = Vector3.Angle(toTarget, cameraForward);
            Vector3 cross = Vector3.Cross(toTarget, cameraForward);

            return (cross.y < 0) ? -angle : angle;
        }
    }
}