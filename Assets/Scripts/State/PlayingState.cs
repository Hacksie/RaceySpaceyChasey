using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private PlayerController player;
        private UI.AbstractPresenter hudPresenter;


        public bool PlayerActionAllowed => true;

        public PlayingState(PlayerController player, UI.AbstractPresenter hudPresenter)
        {
            this.player = player;
            this.hudPresenter = hudPresenter;
        }

        public void Begin()
        {
            this.hudPresenter.Show();
            if(Cursor.visible)
                Cursor.visible = false;            
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
            //GameManager.Instance.SetPause();
        }

        public void Select()
        {

        }
    }
}