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
            this.forwardSpeed = 10;
        }        
    }
}