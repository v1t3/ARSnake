using System;
using UnityEngine;

namespace Game.Scripts.Movement
{
    public class PlayControls3d : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CheckButton();
            }
        }

        private void CheckButton()
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out RaycastHit hit);

            if (hit.rigidbody)
            {
                var button = hit.rigidbody.GetComponent<Button3d>();

                if (button)
                {
                    button.onClick.Invoke();
                }
            }
        }

        public void MoveUp()
        {
            
        }

        public void MoveDown()
        {
            
        }

        public void Moveleft()
        {
            
        }

        public void MoveRight()
        {
            
        }

        public void SetFastSpeed()
        {
            
        }

        public void SetNormalSpeed()
        {
            
        }
    }
}