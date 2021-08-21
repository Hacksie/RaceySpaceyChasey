
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


namespace HackedDesign.UI
{
    public class HudPresenter : AbstractPresenter
    {
        [SerializeField] private UnityEngine.UI.RawImage hudFace;
        [SerializeField] private List<UnityEngine.UI.Image> raceyEmpty;
        [SerializeField] private List<UnityEngine.UI.Image> raceyFull;
        [SerializeField] private List<UnityEngine.UI.Image> spaceyEmpty;
        [SerializeField] private List<UnityEngine.UI.Image> spaceyFull;
        [SerializeField] private List<UnityEngine.UI.Image> chaseyEmpty;
        [SerializeField] private List<UnityEngine.UI.Image> chaseyFull;


        public override void Repaint()
        {
            if (GameManager.Instance.Player && GameManager.Instance.Player.ship)
            {
                hudFace.texture = GameManager.Instance.Player.ship.renderTexture;
                RepaintBars();
            }
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

            for (int i = 0; i < raceyEmpty.Count; i++)
            {
                spaceyEmpty[i].gameObject.SetActive(i < maxSpacey);
            }
            for (int i = 0; i < raceyFull.Count; i++)
            {
                spaceyFull[i].gameObject.SetActive(i < currentSpacey);
            }            

            for (int i = 0; i < raceyEmpty.Count; i++)
            {
                chaseyEmpty[i].gameObject.SetActive(i < maxChasey);
            }
            for (int i = 0; i < raceyFull.Count; i++)
            {
                chaseyFull[i].gameObject.SetActive(i < currentChasey);
            }            

        }
    }

}