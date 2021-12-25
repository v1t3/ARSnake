using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        private GameManager _gameManager;

        [Header("Menu")]
        [SerializeField] private GameObject parentMenu;
        [SerializeField] private Dropdown menuTypeDropdown;
        
        [Header("Panels")]
        [SerializeField] private GameObject startMenuPanel;
        [SerializeField] private GameObject prepareMenuPanel;
        [SerializeField] private GameObject playMenuPanel;
        [SerializeField] private GameObject pauseMenuPanel;
        [SerializeField] private GameObject settingsMenuPanel;
        [SerializeField] private GameObject gameOverMenuPanel;
        
        [Header("Menus")]
        [SerializeField] private GameObject prepareBottomMenu;
        
        [Header("Resource Texts")]
        [SerializeField] private Text pointsCountText;
        [SerializeField] private Text scoreCountText;
        [SerializeField] private Text highScorePlayText;
        [SerializeField] private Text highScoreGameOverText;
        
        [Header("Input")]
        [SerializeField] private GameObject joystick;
        [SerializeField] private Dropdown inputTypeDropdown;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        public void UpdatePointCountText(int value)
        {
            pointsCountText.text = value.ToString();
        }

        public void UpdateScoreText(int value)
        {
            scoreCountText.text = value.ToString();
        }
        
        public void UpdateHighScoreText(int value)
        {
            highScorePlayText.text = value.ToString();
            highScoreGameOverText.text = value.ToString();
        }

        public void ShowPrepareMenu()
        {
            startMenuPanel.SetActive(false);
            prepareMenuPanel.SetActive(true);
            prepareBottomMenu.SetActive(false);
        }

        public void HidePrepareMenu()
        {
            startMenuPanel.SetActive(true);
            prepareMenuPanel.SetActive(false);
        }

        public void ShowPrepareBottomMenu()
        {
            prepareBottomMenu.SetActive(true);
        }

        public void HidePrepareBottomMenu()
        {
            prepareBottomMenu.SetActive(false);
        }

        public void RestartMenus()
        {
            pauseMenuPanel.SetActive(false);
            HidePlayMenu();
            gameOverMenuPanel.SetActive(false);
        }

        public virtual void ShowStartMenu()
        {
            prepareMenuPanel.SetActive(false);
            ShowPlayMenu();
        }

        public void ShowPlayMenu()
        {
            playMenuPanel.SetActive(true);
        }

        public void HidePlayMenu()
        {
            playMenuPanel.SetActive(false);
        }

        public virtual void ShowPauseMenu()
        {
            pauseMenuPanel.SetActive(true);
        }

        public virtual void HidePauseMenu()
        {
            pauseMenuPanel.SetActive(false);
        }

        public void ShowSettingsMenu()
        {
            settingsMenuPanel.SetActive(true);
            startMenuPanel.SetActive(false);
        }

        public void HideSettingsMenu()
        {
            settingsMenuPanel.SetActive(false);
            startMenuPanel.SetActive(true);
        }

        public void ReturnToMainMenu()
        {
            HidePlayMenu();
            pauseMenuPanel.SetActive(false);
            gameOverMenuPanel.SetActive(false);
            startMenuPanel.SetActive(true);
        }

        public void SetInputType(InputType inputType)
        {
            if (inputType == InputType.Buttons)
            {
                EnableJoystick();
            }
            else
            {
                DisableJoystick();
            }

            inputTypeDropdown.value = (int)inputType;
        }

        public virtual void ShowGameOverMenu()
        {
            HidePlayMenu();
            gameOverMenuPanel.SetActive(true);
        }

        public virtual void EnableJoystick()
        {
            if (_gameManager.GetInputType() == InputType.Buttons)
            {
                joystick.gameObject.SetActive(true);
            }
        }

        public virtual void DisableJoystick()
        {
            joystick.gameObject.SetActive(false);
        }

        public void EnableMenu()
        {
            parentMenu.SetActive(true);
        }

        public void DisableMenu()
        {
            parentMenu.SetActive(false);
        }

        public void SetMenuTypeDropdown(int typeId)
        {
            menuTypeDropdown.SetValueWithoutNotify(typeId);
        }
    }
}