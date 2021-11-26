using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts
{
    public class PlaceManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private MenuManager _menuManager;

        [SerializeField] private GameObject marker;
        [SerializeField] private GameObject fieldToPlace;

        private Vector3 _initScale;
        private float _initDistance;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _menuManager = FindObjectOfType<MenuManager>();
        }

        private void Update()
        {
            if (!_gameManager.PrepareMode) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (
                !_gameManager.IsFieldPlaced &&
                Input.touchCount == 1 &&
                Input.GetTouch(0).phase == TouchPhase.Began
            )
            {
                PlaceParentField();
            }
            
            //todo test
            if (!_gameManager.IsFieldPlaced && Input.GetMouseButtonDown(0) && Input.touchCount == 0)
            {
                PlaceParentField();
            }
        }

        private void PlaceParentField()
        {
            fieldToPlace.transform.position = marker.transform.position;
            fieldToPlace.transform.rotation = marker.transform.rotation;
            fieldToPlace.transform.localScale = marker.transform.localScale;
            
            fieldToPlace.SetActive(true);
            _gameManager.IsFieldPlaced = true;
            marker.SetActive(false);
            
            _menuManager.prepareBottomMenu.SetActive(true);
        }

        /**
         * Used in UI
         */
        public void ResetPosition()
        {
            _gameManager.IsFieldPlaced = false;
            
            marker.SetActive(true);
            fieldToPlace.SetActive(false);
            
            _menuManager.prepareBottomMenu.SetActive(false);
        }

        public void DisablePlacement()
        {
            _gameManager.IsFieldPlaced = false;
            
            marker.SetActive(false);
            fieldToPlace.SetActive(false);
            
            _menuManager.prepareBottomMenu.SetActive(false);
        }
    }
}