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
        [SerializeField] private GameObject fieldToPlace;

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
            fieldToPlace.transform.position = marker.transform.position;
            fieldToPlace.transform.rotation = marker.transform.rotation;
            fieldToPlace.transform.localScale = marker.transform.localScale;
            fieldToPlace.SetActive(true);

            _isPlaced = true;
            _gameManager.StartGame();
        }
    }
}