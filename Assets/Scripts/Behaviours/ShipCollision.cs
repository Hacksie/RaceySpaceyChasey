using UnityEngine;

namespace HackedDesign
{
    public class ShipCollision : MonoBehaviour
    {
        AbstractController controller;

        protected void OnCollisionEnter(Collision collider)
        {
            //Debug.Log("Collided with " + collider.gameObject.name, this);
            var controller = GetComponentInParent<AbstractController>();

            if(controller)
            {
                controller.Collided();
            }
        }        
    }
}