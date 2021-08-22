using System.Collections.Generic;
using System.Linq;
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
        [SerializeField] public Transform shipModelParent;
        [SerializeField] public Transform shipModel = null;

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
                UpdateShipPosition();
                UpdateShipRotation();
                UpdateShipLean();

                dollyCart.m_Speed = this.currentSpeed;
            }




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

        public void Collided()
        {
            //Debug.Log("Collision!", this);
            this.currentSpeed = 0;
            shipModelParent.localPosition = new Vector3(shipModelParent.localPosition.x, 0, shipModelParent.localPosition.z);
        }

        protected void Fire()
        {
            if (ship && ship.currentChasey > 0)
            {
                ship.currentChasey--;
                AbstractController target = null;
                switch (ship.currentChaseyType)
                {
                    case "Twin":
                        target = GetRandomForwardTarget();
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 5f), shipModelParent.transform.forward, target.shipModel.transform, currentSpeed);
                        target = GetRandomForwardTarget();
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 5f), shipModelParent.transform.forward, target.shipModel.transform, currentSpeed);
                        break;
                    case "Blue":
                        target = GetFrontTarget();
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 5f), shipModelParent.transform.forward, target.shipModel.transform, currentSpeed);
                        break;
                    case "Mine":
                        break;
                    case "Storm":
                        break;
                    default:
                        target = GetRandomForwardTarget();
                        if (target)
                            Debug.Log("Targetting " + target.ship.pilot);
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 7f), shipModelParent.transform.forward, target ? target.shipModel.transform : null, currentSpeed);
                        break;
                }
            }
        }

        protected AbstractController GetRandomForwardTarget()
        {
            List<AbstractController> ships = new List<AbstractController>();
            ships.Add(GameManager.Instance.Player);
            ships.AddRange(GameManager.Instance.AI);
            ships.Remove(this);
            var filtered = ships.Where(s => s.dollyCart.m_Position >= this.dollyCart.m_Position).ToList();

            Debug.Log("Target count " + filtered.Count);

            return filtered.Count > 0 ? filtered[Random.Range(0, filtered.Count())] : null;
        }

        protected AbstractController GetFrontTarget()
        {
            List<AbstractController> ships = new List<AbstractController>();
            ships.Add(GameManager.Instance.Player);
            ships.AddRange(GameManager.Instance.AI);
            ships.Remove(this);
            var target = ships.OrderByDescending(s => s.dollyCart.m_Position).FirstOrDefault();

            return target;
        }

        protected abstract void UpdateShipAcceleration();
        protected abstract void UpdateShipPosition();
        protected abstract void UpdateShipRotation();
        protected abstract void UpdateShipLean();



    }
}