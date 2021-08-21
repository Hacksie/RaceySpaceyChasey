#nullable enable
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using DG.Tweening;

namespace HackedDesign
{
    public class PlayerController : AbstractController
    {
        private Vector2 inputVector;
        [Header("Settings")]
        [SerializeField] public float movementSpeed = 10;
        [SerializeField] private float rotateSpeed = 340;
        [SerializeField] private float leanAngle = 80;
        [SerializeField] private float leanTime = 33;

        [SerializeField] private bool allowYMovement = false;
        [SerializeField] private Rect clamp = new Rect(0.2f, 0.2f, 0.6f, 0.6f);

        [Header("Referenced GameObjects")]
        [SerializeField] private Camera mainCamera = null;
        [SerializeField] public Transform playerModel = null;
        [SerializeField] public Transform shipModel = null;



        // [Header("Settings")]


        protected new void Awake()
        {
            base.Awake();
            this.mainCamera = GameManager.Instance.MainCamera;
            //this.us = GetComponent<Ship>();
        }

        public void MovementEvent(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                inputVector = context.ReadValue<Vector2>();
                // if (Game.instance.preferences.invertX)
                //     inputVector.x = 0 - inputVector.x;

                // if (Game.instance.preferences.invertY)
                //     inputVector.y = 0 - inputVector.y;
            }
        }

        public void AccelerateEvent(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                inputVector = context.ReadValue<Vector2>();
                // if (Game.instance.preferences.invertX)
                //     inputVector.x = 0 - inputVector.x;

                // if (Game.instance.preferences.invertY)
                //     inputVector.y = 0 - inputVector.y;
            }
        }        

        public void BarrelRollLeftEvent(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                Spin(-1);
            }
        }

        public void BarrelRollRightEvent(InputAction.CallbackContext context)
        {
            if (GameManager.Instance.CurrentState.PlayerActionAllowed)
            {
                Spin(1);
            }
        }        

        public void FireEvent(InputAction.CallbackContext context)
        {
            // if (!GameManager.Instance.CurrentState.PlayerActionAllowed)
            // {
            //     return;
            // }

            // if (context.performed)
            // {
            //     us?.Launch();
            // }
        }

        public void LookEvent(InputAction.CallbackContext context)
        {
            // if (!GameManager.Instance.CurrentState.PlayerActionAllowed)
            // {
            //     rotateDirection = 0f;
            //     return;
            // }
            // if (context.started)
            // {
            //     rotateDirection = context.ReadValue<float>();
            // }
            // else if (context.canceled)
            // {
            //     rotateDirection = 0f;
            // }
        }

        public void MousePositionEvent(InputAction.CallbackContext context)
        {
            // if (!GameManager.Instance.CurrentState.PlayerActionAllowed)
            // {
            //     return;
            // }
            // mousePosition = context.ReadValue<Vector2>();
        }

        public void StartEvent(InputAction.CallbackContext context)
        {
            // if (!GameManager.Instance.CurrentState.PlayerActionAllowed)
            // {
            //     return;
            // }
            // if (context.started)
            // {
            //     GameManager.Instance.CurrentState.Start();
            // }

        }

        private void Spin(int dir)
        {
            if (!DOTween.IsTweening(shipModel))
            {
                shipModel.DOLocalRotate(new Vector3(shipModel.localEulerAngles.x, shipModel.localEulerAngles.y, 360 * -dir), .4f, RotateMode.LocalAxisAdd).SetEase(Ease.OutSine);
            }
        }

        protected override void UpdateShipPosition()
        {
            playerModel.localPosition += new Vector3(inputVector.x, allowYMovement ? inputVector.y : 0) * movementSpeed * Time.deltaTime;

            // Clamp input to the screen view port first
            Vector3 modelViewPos = mainCamera.WorldToViewportPoint(playerModel.position);
            modelViewPos.x = Mathf.Clamp(modelViewPos.x, clamp.xMin, clamp.xMax);
            modelViewPos.y = Mathf.Clamp(modelViewPos.y, clamp.yMin, clamp.yMax);
            Vector3 modelPos = mainCamera.ViewportToWorldPoint(modelViewPos);

            // Then apply any height adjustments
            var height = Mathf.Max(1f, modelPos.y);
            modelPos.y = Mathf.Lerp(modelPos.y, height, Time.deltaTime * movementSpeed);
            playerModel.position = modelPos;
        }

        protected override void UpdateShipRotation()
        {
            playerModel.rotation = Quaternion.RotateTowards(playerModel.rotation, Quaternion.LookRotation(new Vector3(inputVector.x, inputVector.y, 1)), Mathf.Deg2Rad * rotateSpeed * Time.deltaTime);
        }

        protected override void UpdateShipLean()
        {
        // Vector3 targetEulerAngels = target.localEulerAngles;
        // target.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y, Mathf.LerpAngle(targetEulerAngels.z, -axis * leanLimit, lerpTime));

            playerModel.localEulerAngles = new Vector3(playerModel.localEulerAngles.x, playerModel.localEulerAngles.y, Mathf.LerpAngle(playerModel.localEulerAngles.z, -inputVector.x * leanAngle, leanTime * Time.deltaTime));
        }

        protected override void UpdateShipAcceleration()
        {
            this.forwardSpeed = 10;
        }

    }
}