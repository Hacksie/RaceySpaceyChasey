using UnityEngine;

namespace HackedDesign
{
    public abstract class AbstractController : MonoBehaviour
    {
        //public virtual float TurnDirection { get; }
        [Header("Settings")]
        [SerializeField] public float forwardSpeed = 0;
        [SerializeField] public float currentSpeed = 0;

        [Header("Referenced GameObjects")]
        [SerializeField] public Cinemachine.CinemachineDollyCart dollyCart;

        [Header("State")]
        [SerializeField] public Ship ship;

        protected void Awake()
        {
            dollyCart = GetComponent<Cinemachine.CinemachineDollyCart>();
        }

        protected void Update()
        {
            dollyCart.m_Speed = 0;
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                UpdateShipAcceleration();
                dollyCart.m_Speed = forwardSpeed;
            }

            UpdateShipPosition();
            UpdateShipRotation();
            UpdateShipLean();
        }


        protected abstract void UpdateShipAcceleration();
        protected abstract void UpdateShipPosition();
        protected abstract void UpdateShipRotation();
        protected abstract void UpdateShipLean();
    }
}