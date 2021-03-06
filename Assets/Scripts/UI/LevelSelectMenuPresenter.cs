
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class LevelSelectMenuPresenter : AbstractPresenter
    {

        public override void Repaint()
        {
        }

        public void PopulateValues()
        {
            // sfxSlider.value = GameManager.Instance.PlayerPreferences.sfxVolume;
            // musicSlider.value = GameManager.Instance.PlayerPreferences.musicVolume;
        }        

        public void NextEvent()
        {
            Debug.Log("Next");
            GameManager.Instance.SetPlaying();
        }

        public void ReturnEvent()
        {
            Debug.Log("Return");
            GameManager.Instance.SetCharSelectMenu();
        }
    }
}