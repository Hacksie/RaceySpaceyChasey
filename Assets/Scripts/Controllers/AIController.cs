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
        //[SerializeField] public Transform shipModel = null;
        [Header("Settings")]
        [SerializeField] private float decisionSpeed = 10;
        [SerializeField] private LayerMask layerMask;

        [SerializeField] float currentVert = 0;
        [SerializeField] float targetVert = 0;
        float decisionCooldown = 0;

        public void SetStartPosition(float angle)
        {
            currentVert = Random.Range(-3.5f, -5.5f);
            targetVert = Random.Range(-3.5f, -5.5f);
            //shipModel.localPosition = Vector3.zero;
            shipModel.localPosition = new Vector3(shipModel.localPosition.x, Random.Range(-3, -5.5f), shipModel.localPosition.z);
            //shipParent.rotation = Quaternion.identity;
            shipParent.rotation = Quaternion.Euler(0,0,angle);
            //shipParent.Rotate(0, 0, angle, Space.Self);
            //shipModel.up = shipParent.position - shipModel.position;
            //shipParent.up = Vector3.zero;
        }

        protected override void UpdateShipLean()
        {

        }

        protected override void UpdateShipPosition()
        {
            if (ship)
            {
                currentVert = Mathf.Lerp(currentVert, targetVert, Time.deltaTime / ship.decisionSpeed);
            }
            shipModel.localPosition = new Vector3(shipModel.localPosition.x, Mathf.Clamp(currentVert, -6, 6), shipModel.localPosition.z);

            // Dumb avoidance. move toward the center track
            if (Physics.Raycast(shipModel.transform.position, shipModel.transform.forward, 20.0f, layerMask, QueryTriggerInteraction.Ignore))
            {
                //Debug.Log("Avoiding shit", this);
                if (targetVert < 0)
                    targetVert = -1;

                if (targetVert > 0)
                    targetVert = 1;
            }

        }

        protected override void UpdateShipRotation()
        {

        }

        protected override void UpdateShipAcceleration()
        {
            if (ship)
            {
                UpdateAIBehaviour();
                if (this.currentSpeed <= this.maxSpeed)
                {
                    this.currentSpeed += ship.acceleration * Time.deltaTime;
                }
            }
        }

        private void UpdateAIBehaviour()
        {
            if (decisionCooldown >= (ship.decisionSpeed))
            {
                decisionCooldown = 0 - Random.Range(0, 5); // Break up the decisions
                targetVert = Random.Range(-2.5f, -5f);

                if(Random.Range(0, 1.0f) < (ship.aggressionChance * ship.currentRacey))
                {
                    //Debug.Log("Count targets ahead:" + CountTargetsAhead());
                    //Debug.Log("AI chose to boost");
                    Boost(false);
                }

                // Fire a missile!
                if (!GameManager.Instance.PassiveAI)
                {
                    if (Random.Range(0, 1.0f) < (ship.aggressionChance * ship.currentChasey) && GetRandomForwardTarget() != null)
                    {
                        Debug.Log("decided to fire a missile", this);
                        Fire();
                    }
                }
            }

            decisionCooldown += Time.deltaTime;

        }
    }
}