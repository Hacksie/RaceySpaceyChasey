using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private AbstractController player;
        private UI.AbstractPresenter hudPresenter;
        private UI.CountdownPresenter countdownPresenter;

        private float countdown = 4;


        public bool PlayerActionAllowed => true;

        public PlayingState(AbstractController player, UI.AbstractPresenter hudPresenter, UI.CountdownPresenter countdownPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
            this.countdownPresenter = countdownPresenter;
        }

        public void Begin()
        {
            this.hudPresenter.Show();
            this.countdownPresenter.Show();
            this.countdownPresenter.SetText("Ready!");
            if(Cursor.visible)
                Cursor.visible = false;   

            countdown = 4;         
        }

        public void End()
        {
            Cursor.visible = true;
            this.hudPresenter.Hide();
        }

        public void Update()
        {
            Cursor.visible = false;
            this.hudPresenter.Repaint();
            if(countdown >= 0)
            {
                countdown -= Time.deltaTime;
                string text = countdown >3 ? "Ready?" : (countdown < 1 ? "Go!" : countdown.ToString("N0") );
                this.countdownPresenter.SetText(text);
                if(!GameManager.Instance.Racing && countdown < 1)
                {
                    GameManager.Instance.StartRacing();
                }
            }
            if(countdown < 0)
            {
                this.countdownPresenter.Hide();
                GameManager.Instance.IncLapTimer(Time.deltaTime);
            }
            //this.player.Update();
        }


        public void FixedUpdate()
        {
        }

        public void LateUpdate()
        {
            //this.hudPresenter.Repaint();
        }




        public void Start()
        {
            GameManager.Instance.SetPaused();
        }

        public void Select()
        {

        }
    }
}