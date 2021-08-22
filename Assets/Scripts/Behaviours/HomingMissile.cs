using UnityEngine;

namespace HackedDesign
{
    [RequireComponent(typeof(Rigidbody))]
    public class HomingMissile : MonoBehaviour
    {
        public Transform target;
        [SerializeField] private float timeout = 30;
        [SerializeField] private float speed = 40;
        [SerializeField] private float turnSpeed = 10;

        public bool fired = false;
        private float currentSpeed = 0;
        private string pilot = "";

        private Rigidbody rb;

        private float timer = 0;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        void FixedUpdate()
        {
            if (fired && GameManager.Instance.CurrentState.PlayerActionAllowed && GameManager.Instance.Racing)
            {
                rb.velocity = transform.forward * currentSpeed;
                if (target)
                {
                    var targetRotation = Quaternion.LookRotation(target.position - transform.position);

                    rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime));
                }
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    Fizzle();
                }
            }
            else
            {
                rb.velocity = Vector3.zero;
            }

        }

        public void Fire(Transform target, float baseSpeed, string pilot)
        {
            Debug.Log("missile fired");
            this.fired = true;
            this.currentSpeed = speed + baseSpeed;
            this.target = target;
            this.pilot = pilot;
            timer = timeout;
        }

        public void Reset()
        {
            fired = false;
            timer = 0;
            rb.velocity = Vector3.zero;
            this.gameObject.SetActive(false);

        }

        private void Fizzle()
        {
            Debug.Log("Missile fizzled");
            Reset();
        }

        private void OnCollisionEnter(Collision collision)
        {
            var controller = collision.gameObject.GetComponentInParent<AbstractController>();

            Debug.Log("Missile hit " + collision.gameObject.name);

            if (controller)
            {
                if (controller.ship.pilot != this.pilot)
                {
                    controller.Collided();
                    Reset();
                }
            }
            else if (!collision.collider.isTrigger)
            {
                Reset();
                Debug.Log("Missile hit something else");
            }
        }


    }
}