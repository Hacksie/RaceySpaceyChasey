
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class GameOverMenuPresenter : AbstractPresenter
    {
        [SerializeField] private List<UnityEngine.UI.Text> leaderboard;

        public override void Repaint()
        {
            RepaintLeaderboard();
        }

        public void QuitEvent()
        {
            Debug.Log("Quit");
            GameManager.Instance.SetMainMenu();
        }

        // FIXME: This is nasty
        private void RepaintLeaderboard()
        {
            List<AbstractController> ships = new List<AbstractController>(GameManager.Instance.AI);
            ships.Add(GameManager.Instance.Player);
            var ordered = ships.OrderByDescending(s => s.CurrentPosition).ToList();
            for(int i = 0; i < ordered.Count(); i++)
            {
                leaderboard[i].text = (i + 1).ToString("N0") + "." + ordered[i].ship.pilot;
            }
            //= GameManager.Instance.AI.CopyTo
        }

    }
}