
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class PauseMenuPresenter : AbstractPresenter
    {
        public override void Repaint()
        {
            
        }

        public void ResumeEvent()
        {
            Debug.Log("Resume");
            //GameManager.Instance.SetLevelSelectMenu();
        }

        public void QuitEvent()
        {
            Debug.Log("Quit");
            GameManager.Instance.SetMainMenu();
        }


    }
}