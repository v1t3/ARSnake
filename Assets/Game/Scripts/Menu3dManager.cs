using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class Menu3dManager : MenuManager
    {
        [Header("Controls")]
        [SerializeField] private GameObject playControls;

        [Header("Resource Texts")] 
        [SerializeField] private GameObject playInfo;

        public override void ShowStartMenu()
        {
            base.ShowStartMenu();
            playControls.gameObject.SetActive(true);
            playInfo.gameObject.SetActive(true);
        }

        public override void ShowPauseMenu()
        {
            base.ShowPauseMenu();
            playControls.gameObject.SetActive(false);
            playInfo.gameObject.SetActive(false);
            HidePlayMenu();
        }

        public override void HidePauseMenu()
        {
            base.HidePauseMenu();
            playControls.gameObject.SetActive(true);
            playInfo.gameObject.SetActive(true);
            ShowPlayMenu();
        }

        public override void ShowGameOverMenu()
        {
            base.ShowGameOverMenu();
            playControls.gameObject.SetActive(false);
            playInfo.gameObject.SetActive(false);
        }
    }
}