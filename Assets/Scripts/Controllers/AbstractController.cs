using UnityEngine;

namespace HackedDesign
{
    public abstract class AbstractController : MonoBehaviour
    {
        //public virtual float TurnDirection { get; }
        [Header("Settings")]
        [SerializeField] public float forwardSpeed = 10;
        [SerializeField] public float currentSpeed = 0;

        [Header("Referenced GameObjects")]
        [SerializeField] public Cinemachine.CinemachineDollyCart dollyCart;

        void Update()
        {
            dollyCart.m_Speed = 0;
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                UpdatePosition();
                dollyCart.m_Speed = forwardSpeed;
            }

            UpdateShipPosition();
            UpdateShipRotation();
            UpdateShipLean();
            //UpdateCursor();
            //UpdateCameraGimbal();
        }        

        protected void UpdatePosition()
        {
            // var currentPos = transform.position;
            // //currentTime = Time.time - Game.instance.playingStartTime;
            // currentSpeed = forwardSpeed;
            // currentPos.z += currentSpeed * Time.deltaTime;
            // transform.position = currentPos;
        }  

        protected abstract void UpdateShipPosition();
        protected abstract void UpdateShipRotation();
        protected abstract void UpdateShipLean();      
    }
}