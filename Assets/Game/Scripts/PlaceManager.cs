using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Scripts
{
    public class PlaceManager : MonoBehaviour
    {
        private GameManager _gameManager;
        private MarkerController _markerController;
        private MenuManager _menuManager;
        public MenuManager MenuManager
        {
            set => _menuManager = value;
        }
        
        [SerializeField] private GameObject fieldToPlace;

        private Vector3 _initScale;
        private float _initDistance;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _markerController = FindObjectOfType<MarkerController>();
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
            if (
                !_gameManager.IsFieldPlaced &&
                Input.GetMouseButtonDown(0) && 
                Input.touchCount == 0
            )
            {
                PlaceParentField();
            }
        }

        private void PlaceParentField()
        {
            var markerTransform = _markerController.Marker.transform;
            
            fieldToPlace.transform.position = markerTransform.position;
            fieldToPlace.transform.rotation = markerTransform.rotation;
            fieldToPlace.transform.localScale = markerTransform.localScale;

            fieldToPlace.SetActive(true);
            _gameManager.IsFieldPlaced = true;
            _markerController.InstallMarker();

            _menuManager.ShowPrepareBottomMenu();
        }

        /**
         * Used in UI
         */
        public void ResetPosition()
        {
            _gameManager.IsFieldPlaced = false;

            fieldToPlace.SetActive(false);
            _markerController.ResetMarker();

            _menuManager.HidePrepareBottomMenu();
        }

        public void DisablePlacement()
        {
            _gameManager.IsFieldPlaced = false;

            _markerController.DisableMarker();
            fieldToPlace.SetActive(false);

            _menuManager.HidePrepareBottomMenu();
        }
    }
}