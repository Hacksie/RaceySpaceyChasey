using UnityEngine;

namespace HackedDesign
{
    public class Crate : MonoBehaviour
    {
        [SerializeField] private float cooldownTime = 10.0f;
        public string crateType;

        public string[] crateTypeList = { "Boost", "Missile", "Mine", "Twin", "Blue" };
        public SpriteRenderer crateSprite;
        public Sprite boostSprite;
        public Sprite missileSprite;
        public Sprite mineSprite;
        public Sprite twinSprite;
        public Sprite blueSprite;
        public Sprite randomSprite;

        private float cooldown = 0.0f;

        void Start()
        {
            Reset();
        }

        public void Reset()
        {
            if (Random.value < 0.5)
            {
                crateType = crateTypeList[Random.Range(0, crateTypeList.Length)];
            }
            else
            {
                crateType = "Random";
            }

            UpdateSprite();
        }

        public void UpdateSprite()
        {
            switch (crateType)
            {
                case "Boost":
                    crateSprite.sprite = boostSprite;
                    break;
                case "Missile":
                    crateSprite.sprite = missileSprite;
                    break;
                case "Mine":
                    crateSprite.sprite = mineSprite;
                    break;
                case "Twin":
                    crateSprite.sprite = twinSprite;
                    break;
                case "Blue":
                    crateSprite.sprite = blueSprite;
                    break;
                case "Random":
                default:
                    crateSprite.sprite = randomSprite;
                    break;
            }

        }

        void Update()
        {
            if (cooldown < (Time.time - cooldownTime) && crateType == "")
            {
                Reset();
            }
        }

        void OnTriggerEnter(Collider other)
        {
            var controller = other.gameObject.GetComponentInParent<AbstractController>();

            if (controller)
            {

                var calcCrateType = crateType == "Random" ? crateTypeList[Random.Range(0, crateTypeList.Length)] : crateType;

                //Debug.Log("Crate hit " + calcCrateType);

                switch (calcCrateType)
                {
                    case "Boost":
                        controller.IncBoost();
                        break;
                    case "Missile":
                        controller.IncMissile();
                        controller.SetChaseyType("Missile");
                        break;
                    case "Mine":
                        controller.IncMissile();
                        controller.SetChaseyType("Mine");
                        break;
                    case "Twin":
                        controller.IncMissile();
                        controller.SetChaseyType("Twin");
                        break;
                    case "Blue":
                        controller.IncMissile();
                        controller.SetChaseyType("Blue");
                        break;
                    default:
                        controller.IncMissile();
                        controller.SetChaseyType("Missile");
                        break;
                }

                cooldown = Time.time;
                crateType = "";
                crateSprite.sprite = null;

            }
        }
    }
}