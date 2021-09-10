using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace Game
{
    public class MarkerController : MonoBehaviour
    {
        private ARRaycastManager _arRaycastManager;

        [SerializeField] private Camera arCamera;
        
        [SerializeField] private GameObject marker;

        private Vector3 _initScale;
        private float _initDistance;

        private bool _isPlaced;

        private void Awake()
        {
            _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        }

        private void Update()
        {
            LookAtCamera();
            
            if (Input.touchCount == 2)
            {
                ScaleField();
            }
        }

        private void LookAtCamera()
        {
            var hits = new List<ARRaycastHit>();
            _arRaycastManager.Raycast(
                new Vector2((float)Screen.width / 2, (float)Screen.height / 2),
                hits,
                TrackableType.Planes
            );

            if (hits.Count > 0)
            {
                marker.transform.position = hits[0].pose.position;
                marker.SetActive(true);
            }

            var lookAt = arCamera.transform.position - marker.transform.position;
            lookAt.y = 0;
            
            marker.transform.rotation = Quaternion.LookRotation(-lookAt);
        }

        private void ScaleField()
        {
            var touch1 = Input.GetTouch(0);
            var touch2 = Input.GetTouch(1);

            if (
                touch1.phase == TouchPhase.Ended ||
                touch2.phase == TouchPhase.Ended ||
                touch1.phase == TouchPhase.Canceled ||
                touch2.phase == TouchPhase.Canceled
            )
            {
                return;
            }

            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                _initDistance = Vector2.Distance(touch1.position, touch2.position);
                _initScale = marker.transform.localScale;
            }
            else
            {
                var currentDistance = Vector2.Distance(touch1.position, touch2.position);

                if (Mathf.Approximately(_initDistance, 0))
                {
                    return;
                }

                var delta = currentDistance / _initDistance;

                marker.transform.localScale = _initScale * delta;
            }
        }
    }
}