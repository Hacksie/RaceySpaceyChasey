#nullable enable
using UnityEngine;

namespace HackedDesign
{
    public class GameManager : MonoBehaviour
    {
        public const string gameVersion = "1.01";

        [Header("Game")]

        [SerializeField] private UnityEngine.Audio.AudioMixer? masterMixer = null;
        [SerializeField] private AudioSource? menuMusic = null;
        [SerializeField] private AudioSource? playMusic = null;

[Header("UI")]

        //[SerializeField] private UI.HudPresenter? hudPanel = null;
        [SerializeField] private UI.MainMenuPresenter? mainMenuPanel = null;        
        [SerializeField] private UI.CharSelectMenuPresenter? charSelectMenuPanel = null;        
        [SerializeField] private UI.LevelSelectMenuPresenter? levelSelectMenuPanel = null;        

        private IState currentState = new EmptyState();

#pragma warning disable CS8618
        public static GameManager Instance { get; private set; }
#pragma warning restore CS8618     

        public IState CurrentState
        {
            get
            {
                return this.currentState;
            }
            private set
            {
                this.currentState.End();

                this.currentState = value;

                this.currentState.Begin();
            }
        }

        private GameManager() => Instance = this;

        void Start() => Initialization();

        void Update() => CurrentState?.Update();
        void LateUpdate() => CurrentState?.LateUpdate();
        void FixedUpdate() => CurrentState?.FixedUpdate(); 

        public void SetMainMenu() => CurrentState = new MainMenuState(this.menuMusic, this.playMusic, this.mainMenuPanel);
        public void SetCharSelectMenu() => CurrentState = new CharSelectMenuState(this.charSelectMenuPanel);
        public void SetLevelSelectMenu() => CurrentState = new LevelSelectMenuState(this.levelSelectMenuPanel);
        public void SetPlaying() => CurrentState = new PlayingState();
        public void SetPaused() => CurrentState = new PauseState();

        private void Initialization()
        {
            // preferences = new PlayerPreferences(this.masterMixer);
            // preferences.Load();
            // preferences.SetPreferences();
            HideAllUI();
            SetMainMenu();
        }

        private void HideAllUI()
        {
            // this.hudPanel?.Hide();
            this.mainMenuPanel?.Hide();
            this.charSelectMenuPanel?.Hide();
            this.levelSelectMenuPanel?.Hide();
            // this.readyPanel?.Hide();
            // this.crashPanel?.Hide();
            // this.losePanel?.Hide();
            // this.winPanel?.Hide();
            // this.pausePanel?.Hide();
            // this.cursors.ForEach(c => c.SetActive(false));
        }        

    }
}