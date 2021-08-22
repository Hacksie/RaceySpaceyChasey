using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Rigidbody))]
    public class Mine : MonoBehaviour
    {
        public bool fired = false;
        private string pilot = "";

        private Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void Reset()
        {
            fired = false;
            rb.velocity = Vector3.zero;
            this.gameObject.SetActive(false);
        }

        public void Fire(string pilot)
        {
            Debug.Log("mine fired");
            this.fired = true;
            this.pilot = pilot;
            rb.velocity = Vector3.zero;
            rb.Sleep();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var controller = collision.gameObject.GetComponentInParent<AbstractController>();

            //Debug.Log("Missile hit " + collision.gameObject.name);

            if (controller)
            {
                if (controller.ship.pilot != this.pilot)
                {
                    controller.MissileHit();
                    Reset();
                }
            }
            // FIXME: this is causing the missile to immediately collide sometimes, for some reason.
            // else if (!collision.collider.isTrigger)
            // {
            //     Reset();
            //     Debug.Log("Missile hit something else");
            // }
        }
    }
}