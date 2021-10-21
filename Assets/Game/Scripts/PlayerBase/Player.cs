using UnityEngine;

namespace Game.Scripts.PlayerBase
{
    public class Player : MonoBehaviour
    {
        private GameManager _gameManager;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
        
        public void HitTail()
        {
            _gameManager.GameOver();
        }
    }
}