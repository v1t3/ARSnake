using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Movement
{
    public enum FieldDirection
    {
        Forward,
        Left,
        Right,
        Back
    }
    
    public class JoystickControl : MonoBehaviour, IControl
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform gameFieldTransform;
        
        [SerializeField] private SnakeMovement snakeMovement;

        public void MoveUp()
        {
            var direction = GetFieldDirection();

            if (direction == FieldDirection.Forward)
            {
                snakeMovement.MoveUp();
            } else if (direction == FieldDirection.Right)
            {
                snakeMovement.MoveRight();
            } else if (direction == FieldDirection.Left)
            {
                snakeMovement.MoveLeft();
            } else
            {
                snakeMovement.MoveDown();
            }
        }

        public void MoveDown()
        {
            var direction = GetFieldDirection();

            if (direction == FieldDirection.Forward)
            {
                snakeMovement.MoveDown();
            } else if (direction == FieldDirection.Right)
            {
                snakeMovement.MoveLeft();
            } else if (direction == FieldDirection.Left)
            {
                snakeMovement.MoveRight();
            } else
            {
                snakeMovement.MoveUp();
            }
        }

        public void MoveLeft()
        {
            var direction = GetFieldDirection();

            if (direction == FieldDirection.Forward)
            {
                snakeMovement.MoveLeft();
            } else if (direction == FieldDirection.Right)
            {
                snakeMovement.MoveUp();
            } else if (direction == FieldDirection.Left)
            {
                snakeMovement.MoveDown();
            } else
            {
                snakeMovement.MoveRight();
            }
        }

        public void MoveRight()
        {
            var direction = GetFieldDirection();

            if (direction == FieldDirection.Forward)
            {
                snakeMovement.MoveRight();
            } else if (direction == FieldDirection.Right)
            {
                snakeMovement.MoveDown();
            } else if (direction == FieldDirection.Left)
            {
                snakeMovement.MoveUp();
            } else
            {
                snakeMovement.MoveLeft();
            }
        }

        /**
         * Using in SpeedUp Button
         */
        public void SetNormalSpeed()
        {
            snakeMovement.SetNormalStep();
        }

        /**
         * Using in SpeedUp Button
         */
        public void SetFastSpeed()
        {
            snakeMovement.SetFastStep();
        }

        private FieldDirection GetFieldDirection()
        {
            var angle = GetAngleToTarget();

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