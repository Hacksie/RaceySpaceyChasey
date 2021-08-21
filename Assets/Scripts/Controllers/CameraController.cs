using UnityEngine;

namespace HackedDesign
{
    public class CameraController : MonoBehaviour
    {
        private Camera cam;
        private float initFov;

        void Awake()
        {
            this.cam = GameManager.Instance.MainCamera;
            this.initFov = this.cam.fieldOfView;
        }
        void Update()
        {
            cam.fieldOfView = initFov + GameManager.Instance.Player.currentSpeed;
        }

    }
}