using UnityEngine;

namespace Game
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField] private GameObject prepareMenu;

        public void ShowPrepareMenu(bool value)
        {
            if (prepareMenu != null)
            {
                prepareMenu.SetActive(value);
            }
        }
    }
}