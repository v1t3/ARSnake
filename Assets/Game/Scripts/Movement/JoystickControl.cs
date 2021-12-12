using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.Movement
{
    public class JoystickControl : MonoBehaviour, IControl
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform gameFieldTransform;

        [SerializeField] private SnakeMovement snakeMovement;

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