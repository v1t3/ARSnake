using UnityEngine;

namespace Game.Scripts.Movement
{
    public class PlayControls3d : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;

        [SerializeField] private float tapMinTime = 0.5f;

        private bool _isPressed;
        private Button3d _currentButton;

        private float _tapTimer;

        private void Update()
        {
            #region Editor mouse

            if (Input.GetMouseButtonDown(0))
            {
                CheckButton(Input.mousePosition);

                if (_currentButton)
                {
                    _currentButton.onClick.Invoke();
                    _currentButton.onDown.Invoke();
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (_currentButton)
                {
                    _currentButton.onUp.Invoke();
                }

                _currentButton = null;
            }

            #endregion

            #region Mobile touch

            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    var touch = Input.touches[0].position;
                    CheckButton(new Vector3(touch.x, touch.y, 0));

                    if (_currentButton)
                    {
                        _currentButton.onClick.Invoke();
                        _currentButton.onDown.Invoke();
                    }
                }
                else if (
                    Input.touches[0].phase == TouchPhase.Ended ||
                    Input.touches[0].phase == TouchPhase.Canceled
                )
                {
                    if (_currentButton)
                    {
                        _currentButton.onUp.Invoke();
                    }

                    _currentButton = null;
                }
            }

            #endregion
        }

        private void CheckButton(Vector3 position)
        {
            Ray ray = mainCamera.ScreenPointToRay(position);
            Physics.Raycast(ray, out RaycastHit hit);

            if (hit.rigidbody)
            {
                var button = hit.rigidbody.GetComponent<Button3d>();

                if (button)
                {
                    _currentButton = button;
                }
            }
        }
    }
}