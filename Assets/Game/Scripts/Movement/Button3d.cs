using System;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Scripts.Movement
{
    public class Button3d : MonoBehaviour
    {
        public UnityEvent onClick = new UnityEvent();
        public UnityEvent onDown = new UnityEvent();
        public UnityEvent onUp = new UnityEvent();
    }
}