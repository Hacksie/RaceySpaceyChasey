#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

namespace HackedDesign
{
    public class RabbitController : AbstractController
    {
        protected override void UpdateShipAcceleration()
        {
            this.forwardSpeed = 10;
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
    }
}