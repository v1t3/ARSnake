using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class MenuManager : MonoBehaviour
    {
        [Header("Panels")]
        public GameObject startMenuPanel;
        public GameObject prepareMenuPanel;
        public GameObject playMenuPanel;
        public GameObject pauseMenuPanel;
        public GameObject settingsMenuPanel;
        public GameObject gameOverMenuPanel;
        
        [Header("Menus")]
        public GameObject prepareBottomMenu;
        
        [Header("Resource Texts")]
        [SerializeField] private Text pointsCountText;
        [SerializeField] private Text scoreCountText;
        [SerializeField] private Text highScorePlayText;
        [SerializeField] private Text highScoreGameOverText;

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
    }
}