
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class MainMenuPresenter : AbstractPresenter
    {
        [Header("Options")]
        [SerializeField] private UnityEngine.UI.Button quitButton;

        public override void Repaint()
        {
            quitButton.interactable = Application.platform != RuntimePlatform.WebGLPlayer;
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
            GameManager.Instance.SetOptions();
        }

        public void QuitEvent()
        {
            Debug.Log("Quit");
            Application.Quit();
        }
    }
}