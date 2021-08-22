using UnityEngine;

namespace HackedDesign
{
    public abstract class AbstractController : MonoBehaviour
    {
        //public virtual float TurnDirection { get; }
        [Header("Settings")]
        [SerializeField] public float maxSpeed = 20;
        [SerializeField] public float forwardSpeed = 0;
        [SerializeField] public float currentSpeed = 0;

        [Header("Referenced GameObjects")]
        [SerializeField] public Cinemachine.CinemachineDollyCart dollyCart;
        [SerializeField] private Transform shipModelParent;

        [Header("State")]
        [SerializeField] public Ship ship;

        public float CurrentPosition { get { return dollyCart.m_Position; } }

        public void SetShip(Ship shipPrefab)
        {
            for (int i = 0; i < shipModelParent.transform.childCount; i++)
            {
                GameObject.Destroy(shipModelParent.transform.GetChild(i).gameObject);
            }

            var go = GameObject.Instantiate(shipPrefab, shipModelParent.transform.position, Quaternion.identity, shipModelParent);
            var newShip = go.GetComponent<Ship>();
            this.ship = newShip;
        }

        public void Reset()
        {
            this.dollyCart.m_Position = 0;
            currentSpeed = 0;
        }



        protected void Awake()
        {
            dollyCart = GetComponent<Cinemachine.CinemachineDollyCart>();
        }

        protected void Update()
        {
            dollyCart.m_Speed = 0;
            if (GameManager.Instance.CurrentState.PlayerActionAllowed && GameManager.Instance.Racing && ship)
            {
                this.currentSpeed -= ship.decceleration * Time.deltaTime;
                this.currentSpeed = Mathf.Max(this.currentSpeed, 0);

                UpdateShipAcceleration();

                dollyCart.m_Speed = this.currentSpeed;
            }

            UpdateShipPosition();
            UpdateShipRotation();
            UpdateShipLean();


        }

        public void Boost(bool free)
        {
            if (free)
            {
                this.currentSpeed += 5.0f;
            }
            else if (ship && ship.currentRacey > 0)
            {
                ship.currentRacey--;
                this.currentSpeed += 5.0f;
            }
        }

        protected void Fire()
        {
            if (ship && ship.currentChasey > 0)
            {
                ship.currentChasey--;
            }
        }


        protected abstract void UpdateShipAcceleration();
        protected abstract void UpdateShipPosition();
        protected abstract void UpdateShipRotation();
        protected abstract void UpdateShipLean();
    }
}