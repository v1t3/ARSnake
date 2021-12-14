using UnityEngine;

namespace Game.Scripts.Movement
{
    public enum FieldDirection
    {
        Forward,
        Left,
        Right,
        Back
    }
    
    public abstract class Control : MonoBehaviour, IControl
    {
        public abstract void MoveUp();
        public abstract void MoveDown();
        public abstract void MoveLeft();
        public abstract void MoveRight();
        public abstract void SetNormalSpeed();
        public abstract void SetFastSpeed();
    }
}