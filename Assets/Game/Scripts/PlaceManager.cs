using UnityEngine;
using UnityEngine.EventSystems;

namespace Game
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
            if (!_gameManager.prepareMode) return;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            if (!_gameManager.isFieldPlaced && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceParentField();
            }
            
            //todo test
            // if (!_gameManager.isFieldPlaced && Input.GetMouseButtonDown(0) && Input.touchCount == 0)
            // {
            //     PlaceParentField();
            // }
        }

        private void PlaceParentField()
        {
            fieldToPlace.transform.position = marker.transform.position;
            fieldToPlace.transform.rotation = marker.transform.rotation;
            fieldToPlace.transform.localScale = marker.transform.localScale;
            fieldToPlace.SetActive(true);
            _gameManager.isFieldPlaced = true;
            marker.SetActive(false);
            _menuManager.ShowPrepareMenu(true);
        }

        public void ResetPosition()
        {
            fieldToPlace.SetActive(false);
            _gameManager.isFieldPlaced = false;
            marker.SetActive(true);
            _menuManager.ShowPrepareMenu(false);
        }
    }
}