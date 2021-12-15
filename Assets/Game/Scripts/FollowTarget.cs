using UnityEngine;

namespace Game.Scripts
{
    public class FollowTarget : MonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField] private float lerpSpeed = 1f;
        [SerializeField] private float angleDelta;

        private void LateUpdate()
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * lerpSpeed);

            Quaternion toRotation = Quaternion.Euler(0, target.eulerAngles.y, 0);
            float angle = Quaternion.Angle(transform.rotation, toRotation);
            
            if (angle >= angleDelta)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * lerpSpeed);
            }
        }
    }
}