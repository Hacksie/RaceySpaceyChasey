#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class AIController : AbstractController
    {
        [Header("Referenced GameObjects")]
        [SerializeField] public Transform shipParent = null;
        [SerializeField] public Transform shipModel = null;

        public void SetStartPosition(float angle)
        {
            shipModel.localPosition = new Vector3(shipModel.localPosition.x, Random.Range(-3, - 6), shipModel.localPosition.z);
            // FIXME: Carve up the starting space into a pie and give each AI it's own space
            shipParent.Rotate(0, 0, angle, Space.Self);
        }

        protected override void UpdateShipLean()
        {

        }

        protected override void UpdateShipPosition()
        {

        }

        protected override void UpdateShipRotation()
        {

        }

        protected override void UpdateShipAcceleration()
        {
            if (ship)
            {
                if (this.currentSpeed <= this.maxSpeed)
                {
                    this.currentSpeed += ship.acceleration * Time.deltaTime;
                }
            }
        }
    }
}