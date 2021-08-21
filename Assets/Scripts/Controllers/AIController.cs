#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class AIController : AbstractController
    {
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