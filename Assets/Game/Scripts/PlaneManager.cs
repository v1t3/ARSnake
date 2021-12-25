using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace Game.Scripts
{
    public class PlaneManager : MonoBehaviour
    {
        [SerializeField] private ARPlaneManager arPlaneManager;

        [SerializeField] private ARPlane arPlanePrefab;

        [SerializeField] private bool state;

        private void Start()
        {
            UpdateARPlane();
        }

        public void ShowARPlane()
        {
            state = true;
            
            UpdateARPlane();
        }

        public void HideARPlane()
        {
            state = false;
            
            UpdateARPlane();
        }

        private void UpdateARPlane()
        {
            arPlanePrefab.GetComponent<ARPlaneMeshVisualizer>().enabled = state;
            arPlanePrefab.GetComponent<MeshRenderer>().enabled = state;
            arPlanePrefab.GetComponent<LineRenderer>().enabled = state;

            foreach (ARPlane plane in arPlaneManager.trackables)
            {
                plane.gameObject.GetComponent<ARPlaneMeshVisualizer>().enabled = state;
                plane.gameObject.GetComponent<MeshRenderer>().enabled = state;
                plane.gameObject.GetComponent<LineRenderer>().enabled = state;
            }
        }
    }
}