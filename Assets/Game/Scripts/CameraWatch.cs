using UnityEngine;

namespace Game
{
    public class CameraWatch : MonoBehaviour
    {
        [SerializeField] private Camera arCamera;

        private void Update()
        {
            transform.position = arCamera.transform.position;
            transform.rotation = arCamera.transform.rotation;
        }
    }
}