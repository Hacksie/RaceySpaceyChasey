
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class MainMenuPresenter : AbstractPresenter
    {

        [SerializeField] private AudioMixer masterMixer = null;


        [Header("Options")]
        [SerializeField] private UnityEngine.UI.Slider sfxSlider = null;
        [SerializeField] private UnityEngine.UI.Slider musicSlider = null;

        public override void Repaint()
        {
        }

        public void PopulateValues()
        {
            // sfxSlider.value = GameManager.Instance.PlayerPreferences.sfxVolume;
            // musicSlider.value = GameManager.Instance.PlayerPreferences.musicVolume;
        }        

        public void PlayEvent()
        {
            Debug.Log("Play");
            GameManager.Instance.SetCharSelectMenu();
        }

        public void OptionsEvent()
        {
            Debug.Log("Options");
        }

        public void QuitEvent()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}