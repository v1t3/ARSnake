using UnityEngine;

namespace Game.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PlaneManager planeManager;
        [SerializeField] private PlaceManager placeManager;

        [Header("Panels")]
        [SerializeField] private GameObject startMenuPanel;
        [SerializeField] private GameObject prepareMenuPanel;
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private GameObject settingsMenuPanel;
        [SerializeField] private GameObject playMenuPanel;
        [SerializeField] private GameObject gameOverMenuPanel;
        
        [Header("Menus")]
        [SerializeField] private GameObject prepareMenu;

        public void StartGame()
        {
            startMenuPanel.SetActive(false);
            prepareMenuPanel.SetActive(true);
            gameManager.SetPrepareMode(true);
            planeManager.SetARPlane(true);
        }

        public void ShowSettingsInStart()
        {
            startMenuPanel.SetActive(false);
            settingsMenuPanel.SetActive(true);
        }

        public void HideSettingsInStart()
        {
            settingsMenuPanel.SetActive(false);
            startMenuPanel.SetActive(true);
        }

        public void ShowPrepareMenu(bool value)
        {
            if (prepareMenu != null)
            {
                prepareMenu.SetActive(value);
            }
        }

        public void Play()
        {
            prepareMenuPanel.SetActive(false);
            playMenuPanel.SetActive(true);
            gameManager.StartPlay();
            planeManager.SetARPlane(false);
        }

        public void ResetField()
        {
            placeManager.ResetPosition();
        }

        public void PausePlay()
        {
            playMenuPanel.SetActive(false);
            pauseMenuPanel.SetActive(true);
            gameManager.TogglePausePlay();
        }

        public void ContinuePlay()
        {
            pauseMenuPanel.SetActive(false);
            playMenuPanel.SetActive(true);
            gameManager.TogglePausePlay();
        }

        public void TogglePausePlay(bool value)
        {
            playMenuPanel.SetActive(!value);
            pauseMenuPanel.SetActive(value);
        }

        public void RestartGame()
        {
            gameManager.Reload();
        }

        public void ReturnToMainMenu()
        {
            // gameOverMenuPanel.SetActive(false);
            gameManager.Reload();
        }

        public void ShowGameOverMenu()
        {
            playMenuPanel.SetActive(false);
            gameOverMenuPanel.SetActive(true);
        }

        public void ExitGame()
        {
            gameManager.Exit();
        }
    }
}