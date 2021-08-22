using UnityEngine;

namespace HackedDesign
{
    public class Crate : MonoBehaviour
    {
        public string crateType;

        public string[] crateTypeList = { "Boost", "Missile", "Mine", "Twin", "Blue" };
        public SpriteRenderer crateSprite;
        public Sprite boostSprite;
        public Sprite missileSprite;
        public Sprite mineSprite;
        public Sprite twinSprite;
        public Sprite blueSprite;
        public Sprite randomSprite;

        void Begin()
        {

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

        }

        void OnTriggerEnter(Collider other)
        {
            var controller = GetComponentInParent<AbstractController>();

            if (controller)
            {
                var calcCrateType = crateType == "Random" ? crateTypeList[Random.Range(0, crateTypeList.Length)] : crateType;

                switch (calcCrateType)
                {
                    case "Boost":

                        break;

                    case "Missile":
                    default:
                        // Inc missile
                        //controller.Collided();
                        break;
                }

            }
        }
    }
}