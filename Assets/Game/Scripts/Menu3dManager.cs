using UnityEngine;

namespace Game.Scripts
{
    public class Menu3dManager : MenuManager
    {
        [Header("Resource Texts")] 
        [SerializeField] private GameObject playInfo;

        public override void ShowStartMenu()
        {
            base.ShowStartMenu();
            EnableJoystick();
            playInfo.gameObject.SetActive(true);
        }

        public override void ShowPauseMenu()
        {
            base.ShowPauseMenu();
            DisableJoystick();
            playInfo.gameObject.SetActive(false);
            HidePlayMenu();
        }

        public override void HidePauseMenu()
        {
            base.HidePauseMenu();
            EnableJoystick();
            playInfo.gameObject.SetActive(true);
            ShowPlayMenu();
        }

        public override void ShowGameOverMenu()
        {
            base.ShowGameOverMenu();
            DisableJoystick();
            playInfo.gameObject.SetActive(false);
        }
    }
}