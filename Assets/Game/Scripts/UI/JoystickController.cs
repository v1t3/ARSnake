using UnityEngine;

namespace Game.UI
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform gameFieldTransform;
        [SerializeField] private Transform joystickTransform;

        private void Update()
        {
            RotateJoystick();
        }

        private void RotateJoystick()
        {
            Vector3 toField = gameFieldTransform.forward;
            toField.y = 0;
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0;

            float angle = Vector3.Angle(toField, cameraForward);
            Vector3 cross = Vector3.Cross(toField, cameraForward);
            
            if (cross.y < 0)
            {
                angle = -angle;
            }
            
            joystickTransform.localRotation = Quaternion.Euler(0,0,angle);
        }
    }
}