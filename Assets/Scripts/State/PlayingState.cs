using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private AbstractController player;
        private UI.AbstractPresenter hudPresenter;
        private UI.CountdownPresenter countdownPresenter;
        private LineRenderer lineRenderer;
        private Cinemachine.CinemachineSmoothPath path;

        private float countdown = 4;


        public bool PlayerActionAllowed => true;

        public PlayingState(AbstractController player, LineRenderer lineRenderer, Cinemachine.CinemachineSmoothPath path, UI.AbstractPresenter hudPresenter, UI.CountdownPresenter countdownPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
            this.countdownPresenter = countdownPresenter;
            this.lineRenderer = lineRenderer;
            this.path = path;
        }

        public void Begin()
        {
            this.hudPresenter.Show();
            this.countdownPresenter.Show();
            this.countdownPresenter.SetText("Ready!");
            // if(Cursor.visible)
            //     Cursor.visible = false;   

            countdown = 4;

            float slice = 360 / 6;

            for (int i = 0; i < GameManager.Instance.AI.Count; i++)
            {
                //AIController ai = GameManager.Instance.AI[i];
                GameManager.Instance.AI[i].SetStartPosition((i + 1) * slice);
            }

            

            var points = path.m_Waypoints.Select(e => e.position);
            lineRenderer.positionCount = points.Count();
            lineRenderer.SetPositions(points.ToArray());
        }

        public void End()
        {
            Cursor.visible = true;
            this.hudPresenter.Hide();
        }

        public void Update()
        {
            //Cursor.visible = false;
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