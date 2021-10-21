using UnityEngine;

namespace Game.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameManager gameManager;
        [SerializeField] private PlaneManager planeManager;
        [SerializeField] private PlaceManager placeManager;

        [Space]
        [SerializeField] private GameObject startMenuPanel;
        [SerializeField] private GameObject prepareMenuPanel;
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private GameObject settingsMenuPanel;
        [SerializeField] private GameObject gameMenuPanel;
        [SerializeField] private GameObject gameOverMenuPanel;
        
        [Space]
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
            gameMenuPanel.SetActive(true);
            gameManager.StartPlay();
            planeManager.SetARPlane(false);
        }

        public void ResetField()
        {
            placeManager.ResetPosition();
        }

        public void PausePlay()
        {
            gameMenuPanel.SetActive(false);
            pauseMenuPanel.SetActive(true);
        }

        public void ContinuePlay()
        {
            pauseMenuPanel.SetActive(false);
            gameMenuPanel.SetActive(true);
        }

        public void TogglePausePlay(bool value)
        {
            gameMenuPanel.SetActive(!value);
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
            gameMenuPanel.SetActive(false);
            gameOverMenuPanel.SetActive(true);
        }

        public void ExitGame()
        {
            gameManager.Exit();
        }
    }
}