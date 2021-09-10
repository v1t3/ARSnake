using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Game
{
    public class PlaceManager : MonoBehaviour
    {
        private GameManager _gameManager;

        [SerializeField] private GameObject marker;
        [SerializeField] private GameObject parent;

        private Vector3 _initScale;
        private float _initDistance;

        private bool _isPlaced;

        private void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (!_isPlaced && Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                PlaceParentField();
            }
        }

        private void PlaceParentField()
        {
            parent.transform.position = marker.transform.position;
            parent.transform.rotation = marker.transform.rotation;
            parent.transform.localScale = marker.transform.localScale;
            parent.SetActive(true);

            _isPlaced = true;
            _gameManager.StartGame();
        }
    }
}