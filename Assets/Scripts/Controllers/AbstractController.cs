using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
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
        [SerializeField] public float boostCooldown = 2f;
        [SerializeField] public float missileCooldown = 2f;

        [Header("Referenced GameObjects")]
        [SerializeField] public Cinemachine.CinemachineDollyCart dollyCart;
        [SerializeField] public Transform shipModelParent;
        [SerializeField] public Transform shipModel = null;

        [Header("State")]
        [SerializeField] public Ship ship;

        private float lastBoostTime = 0;
        private float lastMissileTime = 0;

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
            else if (ship && ship.currentRacey > 0 && lastBoostTime <= (Time.time - boostCooldown))
            {
                ship.currentRacey--;
                this.currentSpeed += 5.0f;
                lastBoostTime = Time.time;
            }
        }

        protected void Fire()
        {
            if (ship && ship.currentChasey > 0 && lastBoostTime <= (Time.time - boostCooldown) && GameManager.Instance.CurrentState.PlayerActionAllowed && GameManager.Instance.Racing)
            {
                //FIXME: Remove this on test complete
                ship.currentChasey--;
                lastBoostTime = Time.time;
                AbstractController target = null;
                switch (ship.currentChaseyType)
                {
                    case "Twin":
                        Debug.Log("Firing twins");
                        target = GetRandomForwardTarget();
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 3f), shipModelParent.transform.forward, target ? target.shipModel.transform : null, currentSpeed, ship.pilot);
                        target = GetRandomForwardTarget();
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 3f), shipModelParent.transform.forward, target ? target.shipModel.transform : null, currentSpeed, ship.pilot);
                        ship.currentChaseyType = "Missile";
                        break;
                    case "Blue":
                        Debug.Log("Firing big blue");
                        target = GetFrontTarget();
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 3f), shipModelParent.transform.forward, target ? target.shipModel.transform : null, currentSpeed, ship.pilot);
                        ship.currentChaseyType = "Missile";
                        break;
                    case "Mine":
                        Debug.Log("Firing mine");
                        ship.currentChaseyType = "Missile";
                        break;
                    case "Storm":
                        break;
                    default:
                        Debug.Log("firing missile");
                        target = GetRandomForwardTarget();
                        if (target)
                            Debug.Log("Targetting " + target.ship.pilot);
                        GameManager.Instance.MissilePool.Fire(shipModelParent.transform.position + (shipModelParent.transform.forward * 3f), shipModelParent.transform.forward, target ? target.shipModel.transform : null, currentSpeed, ship.pilot);
                        break;
                }
            }
        }

        public void SetChaseyType(string type)
        {
            Debug.Log("Set Chasey Type: " + type, this);
            ship.currentChaseyType = type;
        }

        public void IncBoost()
        {
            Debug.Log("Boost increased", this);
            ship.currentRacey = Mathf.Min(ship.currentRacey + 1, ship.maxRacey);
        }

        public void IncMissile()
        {
            Debug.Log("Missiles increased", this);
            ship.currentChasey = Mathf.Min(ship.currentChasey + 1, ship.maxChasey);
        }

        public void MissileHit()
        {
            if (DOTween.IsTweening(shipModel))
            {
                Debug.Log("Barrel rolled out of it!");
                return;
            }
            Collided();
        }

        public void Collided()
        {
            //Debug.Log("Collision!", this);
            this.currentSpeed = 0;
            shipModelParent.localPosition = new Vector3(shipModelParent.localPosition.x, 0, shipModelParent.localPosition.z);
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