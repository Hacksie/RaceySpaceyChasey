using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PauseState : IState
    {
        // private PlayerController player;
        // private List<Ship> ships;
        // private List<GameObject> cursors;
        private UI.AbstractPresenter pausePresenter;
        // private ObstaclePool obstaclePool;
        // private PropsPool propsPool;
        // private AudioSource music;

        public bool PlayerActionAllowed => false;


        //public PlayingState(PlayerController player, List<Ship> ships, List<GameObject> cursors, ObstaclePool obstaclePool,PropsPool propsPool, AudioSource music, UI.AbstractPresenter hudPresenter)
        public PauseState(UI.AbstractPresenter pausePresenter)
        {
            this.pausePresenter = pausePresenter;
            // this.player = player;
            // this.ships = ships;
            // this.cursors = cursors;
            // this.music = music;
            // this.obstaclePool = obstaclePool;
            // this.propsPool = propsPool;
            // this.hudPresenter = hudPresenter;
        }


        public void Begin()
        {
            if(!Cursor.visible)
                Cursor.visible = true;            
            // this.ships.ForEach(s => s.Begin());
            //this.hudPresenter.Show();
        }

        public void End()
        {
            Cursor.visible = true;
            //this.hudPresenter.Hide();
        }

        public void Update()
        {
            Cursor.visible = false;
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