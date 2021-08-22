using UnityEngine;

namespace HackedDesign
{
    public class OptionsState : IState
    {
        private UI.AbstractPresenter optionsPresenter;

        public bool PlayerActionAllowed => false;

        public OptionsState(UI.AbstractPresenter optionsPresenter)
        {
            this.optionsPresenter = optionsPresenter;

        }

        public void Begin()
        {
            this.optionsPresenter.Show();
            this.optionsPresenter.Repaint();
        }

        public void End()
        {
            this.optionsPresenter.Hide();
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