using UnityEngine;

namespace Game.Scripts
{
    public class Food : MonoBehaviour
    {
        [SerializeField] private int points = 100;

        [HideInInspector]
        public Vector2Int selfPosition;
        
        public int Points => points;
    }
}
