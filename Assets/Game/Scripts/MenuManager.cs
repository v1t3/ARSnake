using UnityEngine;

namespace Game.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PlaceManager placeManager;

        [Header("Panels")]
        public GameObject startMenuPanel;
        public GameObject prepareMenuPanel;
        public GameObject playMenuPanel;
        public GameObject pauseMenuPanel;
        public GameObject settingsMenuPanel;
        public GameObject gameOverMenuPanel;
        
        [Header("Menus")]
        public GameObject prepareBottomMenu;
    }
}