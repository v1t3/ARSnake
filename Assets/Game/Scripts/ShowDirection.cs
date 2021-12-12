using System;
using UnityEngine;

namespace Game.Scripts
{
    public class ShowDirection : MonoBehaviour
    {
        private void Update()
        {
            Debug.DrawRay(transform.position, transform.up, Color.red);
        }
    }
}