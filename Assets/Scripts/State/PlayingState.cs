using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    public class PlayingState : IState
    {
        private PlayerController player;
        // private List<Ship> ships;
        // private List<GameObject> cursors;
        // private UI.AbstractPresenter hudPresenter;
        // private ObstaclePool obstaclePool;
        // private PropsPool propsPool;
        // private AudioSource music;

        public bool PlayerActionAllowed => true;


        //public PlayingState(List<Ship> ships, List<GameObject> cursors, ObstaclePool obstaclePool,PropsPool propsPool, AudioSource music, UI.AbstractPresenter hudPresenter)
        public PlayingState(PlayerController player)
        {
            this.player = player;
            // this.ships = ships;
            // this.cursors = cursors;
            // this.music = music;
            // this.obstaclePool = obstaclePool;
            // this.propsPool = propsPool;
            // this.hudPresenter = hudPresenter;
        }


        public void Begin()
        {
            // this.ships.ForEach(s => s.Begin());
            //this.hudPresenter.Show();
            if(Cursor.visible)
                Cursor.visible = false;            
        }

        public void End()
        {
            Cursor.visible = true;
            //this.hudPresenter.Hide();
        }

        public void Update()
        {
            Cursor.visible = false;
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