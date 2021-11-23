using UnityEngine;

namespace Game.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Managers")]
        [SerializeField] private GameManager gameManager;
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
            gameManager.BeginPrepare();
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
            prepareMenu.SetActive(value);
        }

        public void Play()
        {
            prepareMenuPanel.SetActive(false);
            gameManager.StartPlay();
        }

        public void ResetField()
        {
            placeManager.ResetPosition();
        }

        public void PausePlay()
        {
            ShowPlayMenu(false);
            pauseMenuPanel.SetActive(true);
            gameManager.TogglePausePlay();
        }

        public void ContinuePlay()
        {
            pauseMenuPanel.SetActive(false);
            ShowPlayMenu(true);
            gameManager.TogglePausePlay();
        }

        public void TogglePausePlay(bool value)
        {
            ShowPlayMenu(!value);
            pauseMenuPanel.SetActive(value);
        }

        public void RestartGame()
        {
            pauseMenuPanel.SetActive(false);
            gameManager.Reload();
        }

        public void ReturnToMainMenu()
        {
            gameManager.ReturnToMainMenu();
        }

        public void ShowPlayMenu(bool value)
        {
            playMenuPanel.SetActive(value);
        }

        public void ShowGameOverMenu()
        {
            ShowPlayMenu(false);
            gameOverMenuPanel.SetActive(true);
        }

        public void ExitGame()
        {
            gameManager.Exit();
        }
    }
}