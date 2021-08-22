using UnityEngine;

namespace HackedDesign
{
    public class GameOverState : IState
    {
        private UI.AbstractPresenter gameOverPresenter;

        public bool PlayerActionAllowed => false;

        public GameOverState(UI.AbstractPresenter gameOverPresenter)
        {
            this.gameOverPresenter = gameOverPresenter;

        }

        public void Begin()
        {
            this.gameOverPresenter.Show();
            this.gameOverPresenter.Repaint();
        }

        public void End()
        {
            this.gameOverPresenter.Hide();
        }

  
        public void FixedUpdate()
        {

        }

        public void LateUpdate()
        {
            
        }

   
        public void Start()
        {
            
        }

        public void Select()
        {

        }        


        public void Update()
        {

        }
    }
}