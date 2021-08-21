#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HackedDesign
{
    public class GameManager : MonoBehaviour
    {
        public const string gameVersion = "1.01";

        [Header("Game")]
        [SerializeField] private Camera? mainCamera = null;
        [SerializeField] private AbstractController? player = null;
        [SerializeField] private UnityEngine.Audio.AudioMixer? masterMixer = null;
        [SerializeField] private AudioSource? menuMusic = null;
        [SerializeField] private AudioSource? playMusic = null;
        [SerializeField] private List<Ship> shipPrefabs = null;
        [SerializeField] private List<AIController> ai = null;
        [SerializeField] private List<Color> aiColors = null;

        [Header("UI")]
        [SerializeField] private UI.HudPresenter? hudPanel = null;
        [SerializeField] private UI.MainMenuPresenter? mainMenuPanel = null;
        [SerializeField] private UI.CharSelectMenuPresenter? charSelectMenuPanel = null;
        [SerializeField] private UI.LevelSelectMenuPresenter? levelSelectMenuPanel = null;
        [SerializeField] private UI.PauseMenuPresenter? pauseMenuPanel = null;
        [SerializeField] private UI.CountdownPresenter? countdownPanel = null;

        [Header("State")]
        [SerializeField] private bool racing = false;
        [SerializeField] private float lapTimer = 0.0f;
        [SerializeField] private int currentLap = 1;
        [SerializeField] private int maxLaps = 3;

        private IState currentState = new EmptyState();

#pragma warning disable CS8618
        public static GameManager Instance { get; private set; }
#pragma warning restore CS8618

        public Camera? MainCamera { get { return mainCamera; } private set { mainCamera = value; } }
        public AbstractController? Player { get { return player; } private set { player = value; } }
        public List<AIController> AI { get { return ai; } private set { ai = value; } }
        public List<Ship> Ships { get { return shipPrefabs; } }
        public List<Color> AIColors { get { return aiColors; }}
        public bool Racing { get { return racing;} private set { racing = value; }}
        public float LapTimer { get { return lapTimer; } private set { lapTimer = value; }}
        public int CurrentLap { get { return currentLap; } private set { currentLap = value; }}
        public int MaxLaps { get { return maxLaps;} private set { maxLaps = value;}}

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
        public void SetPlaying() => CurrentState = new PlayingState(this.player, this.hudPanel, this.countdownPanel);
        public void SetPaused() => CurrentState = new PauseState(this.pauseMenuPanel);

        public void StartRacing() {
            Debug.Log("Race started!");
            racing = true;
        }

        public void StopRacing() {
            Debug.Log("Race ended!");
            racing = false;
        }

        public void Reset()
        {
            racing = false;
            lapTimer = 0.0f;
            currentLap = 1;
            maxLaps = 3;
            player?.Reset();
            ResetAI();
        }

        public void ResetAI()
        {
            for(int i=0;i< ai.Count; i++)
            {
                ai[i].Reset();
            }
        }

        public void RandomizeAI()
        {
            List<Ship> remaining = shipPrefabs.Where(s => s.pilot != Player.ship.pilot).ToList();

            for (int i = 0; i < ai.Count; i++)
            {
                var chosen = remaining[Random.Range(0, remaining.Count)];
                ai[i].SetShip(chosen);
                Debug.Log("AI " + i + ":" + chosen.pilot);
                remaining.Remove(chosen);
            }
        }

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
            this.hudPanel?.Hide();
            this.mainMenuPanel?.Hide();
            this.charSelectMenuPanel?.Hide();
            this.levelSelectMenuPanel?.Hide();
            this.pauseMenuPanel?.Hide();
            this.countdownPanel?.Hide();
        }
    }
}