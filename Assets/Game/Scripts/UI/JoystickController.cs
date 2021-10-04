using System;
using UnityEngine;

namespace UI
{
    public class JoystickController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform gameFieldTransform;
        [SerializeField] private Transform joystickTransform;

        private void Update()
        {
            var rot = gameFieldTransform.position - cameraTransform.position;
            Quaternion toField = Quaternion.LookRotation(rot);
            
            // Debug.Log("rot " + rot);
            // Debug.Log("toField " + toField);

            joystickTransform.localRotation = new Quaternion(0,0,toField.y,toField.w);
        }
    }
}