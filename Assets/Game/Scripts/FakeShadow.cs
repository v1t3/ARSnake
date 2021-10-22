using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts
{
    public class FakeShadow : MonoBehaviour
    {
        [SerializeField] private Transform shadow;
        
        [SerializeField] private LayerMask layerMask;

        private void Start()
        {
            Ray ray = new Ray(shadow.transform.position, Vector3.down);
            Physics.Raycast(ray, out RaycastHit hit, 100f, layerMask);

            if (hit.collider)
            {
                var newPos = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                shadow.transform.position = newPos;
            }
        }
    }
}