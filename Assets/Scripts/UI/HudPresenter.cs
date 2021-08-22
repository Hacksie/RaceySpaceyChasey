
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;


namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] private UnityEngine.UI.RawImage hudFace;
        [SerializeField] private UnityEngine.UI.Text speed;
        [SerializeField] private List<UnityEngine.UI.Image> raceyEmpty;
        [SerializeField] private List<UnityEngine.UI.Image> raceyFull;
        [SerializeField] private List<UnityEngine.UI.Image> spaceyEmpty;
        [SerializeField] private List<UnityEngine.UI.Image> spaceyFull;
        [SerializeField] private List<UnityEngine.UI.Image> chaseyEmpty;
        [SerializeField] private List<UnityEngine.UI.Image> chaseyFull;
        [SerializeField] private List<UnityEngine.UI.Text> leaderboard;
        [SerializeField] private UnityEngine.UI.Text laptimerText;
        [SerializeField] private UnityEngine.UI.Text lapText;

        [SerializeField] private UnityEngine.UI.Image chaseyType;
        [SerializeField] private Sprite boostSprite;
        [SerializeField] private Sprite missileSprite;
        [SerializeField] private Sprite mineSprite;
        [SerializeField] private Sprite twinSprite;
        [SerializeField] private Sprite blueSprite;
        [SerializeField] private Sprite randomSprite;

        public override void Repaint()
        {
            if (GameManager.Instance.Player && GameManager.Instance.Player.ship)
            {
                hudFace.texture = GameManager.Instance.Player.ship.renderTexture;
                RepaintBars();
                RepaintLeaderboard();
                RepaintChaseyType();
                speed.text = GameManager.Instance.Player.currentSpeed.ToString("N0");
                laptimerText.text = GameManager.Instance.LapTimer.ToString("N0");
                lapText.text = GameManager.Instance.CurrentLap.ToString("N0") + "/" + GameManager.Instance.MaxLaps.ToString("N0");
            }
        }

        private void RepaintChaseyType()
        {
            switch(GameManager.Instance.Player.ship.currentChaseyType)
            {
                case "Boost":
                    chaseyType.sprite = boostSprite;
                    break;
                case "Missile":
                    chaseyType.sprite = missileSprite;
                    break;
                case "Mine":
                    chaseyType.sprite = mineSprite;
                    break;
                case "Twin":
                    chaseyType.sprite = twinSprite;
                    break;
                case "Blue":
                    chaseyType.sprite = blueSprite;
                    break;
                case "Random":
                default:
                    chaseyType.sprite = randomSprite;
                    break;                
            }

        }

        // FIXME: This is nasty
        private void RepaintLeaderboard()
        {
            //Debug.Log("Sort leaderboard");
            List<AbstractController> ships = new List<AbstractController>(GameManager.Instance.AI);
            ships.Add(GameManager.Instance.Player);
            var ordered = ships.OrderByDescending(s => s.CurrentPosition).ToList();
            for(int i = 0; i < ordered.Count(); i++)
            {
                leaderboard[i].text = (i + 1).ToString("N0") + "." + ordered[i].ship.pilot;
            }
            //= GameManager.Instance.AI.CopyTo
        }

        private void RepaintBars()
        {
            int maxRacey = GameManager.Instance.Player.ship.maxRacey;
            int currentRacey = GameManager.Instance.Player.ship.currentRacey;
            int maxSpacey = GameManager.Instance.Player.ship.maxSpacey;
            int currentSpacey = GameManager.Instance.Player.ship.currentSpacey;
            int maxChasey = GameManager.Instance.Player.ship.maxChasey;
            int currentChasey = GameManager.Instance.Player.ship.currentChasey;

            for (int i = 0; i < raceyEmpty.Count; i++)
            {
                raceyEmpty[i].gameObject.SetActive(i < maxRacey);
            }
            for (int i = 0; i < raceyFull.Count; i++)
            {
                raceyFull[i].gameObject.SetActive(i < currentRacey);
            }

            // for (int i = 0; i < raceyEmpty.Count; i++)
            // {
            //     spaceyEmpty[i].gameObject.SetActive(i < maxSpacey);
            // }
            // for (int i = 0; i < raceyFull.Count; i++)
            // {
            //     spaceyFull[i].gameObject.SetActive(i < currentSpacey);
            // }            

            for (int i = 0; i < chaseyEmpty.Count; i++)
            {
                chaseyEmpty[i].gameObject.SetActive(i < maxChasey);
            }
            for (int i = 0; i < chaseyEmpty.Count; i++)
            {
                chaseyFull[i].gameObject.SetActive(i < currentChasey);
            }            

        }
    }

}