using UnityEngine;

namespace HackedDesign
{
    public class BoostGate : MonoBehaviour
    {
        public void OnTriggerEnter(Collider other)
        {
            
            var controller = other.gameObject.GetComponentInParent<AbstractController>();

            if(controller)
            {
                //Debug.Log("boost! " + controller.gameObject.name);
                controller.Boost(true);
            }

        }
    }
}