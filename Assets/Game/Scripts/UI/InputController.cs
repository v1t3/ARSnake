using Game.Scripts.Units;
using UnityEngine;

namespace Game.Scripts.UI
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform gameFieldTransform;

        [SerializeField] private SnakeMovement movement;
    }
}