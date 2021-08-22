
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class OptionsMenuPresenter : AbstractPresenter
    {
        [SerializeField] AudioMixer masterAudioMixer;
        [SerializeField] AudioSource sampleSFX;
        [SerializeField] UnityEngine.UI.Slider masterSlider;
        [SerializeField] UnityEngine.UI.Slider musicSlider;
        [SerializeField] UnityEngine.UI.Slider sfxSlider;

        public override void Repaint()
        {
            float master, music, sfx;
            masterAudioMixer.GetFloat("Master", out master);
            masterAudioMixer.GetFloat("Music", out music);
            masterAudioMixer.GetFloat("SFX", out sfx);

            masterSlider.value = master;
            musicSlider.value = music;
            sfxSlider.value = sfx;
            
        }

        public void MasterChangedEvent()
        {
            float master = masterSlider.value;
            masterAudioMixer.SetFloat("Master", master);

        }

        public void MusicChangedEvent()
        {
            float music = musicSlider.value;
            masterAudioMixer.SetFloat("Music", music);
        }

        public void SFXChangedEvent()
        {
            float sfx = sfxSlider.value;
            masterAudioMixer.SetFloat("SFX", sfx);
            if(sampleSFX)
                sampleSFX.Play();
        }

        public void QuitEvent()
        {
            GameManager.Instance.SetMainMenu();
        }
    }
}