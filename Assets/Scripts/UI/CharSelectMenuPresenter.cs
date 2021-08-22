
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class CharSelectMenuPresenter : AbstractPresenter
    {
        [SerializeField] private UnityEngine.UI.Text thegirlSelectedText;
        [SerializeField] private UnityEngine.UI.Text tmin8orSelectedText;
        [SerializeField] private UnityEngine.UI.Text legeneralSelectedText;
        [SerializeField] private UnityEngine.UI.Text dapperSelectedText;
        [SerializeField] private UnityEngine.UI.Text robinSelectedText;
        [SerializeField] private UnityEngine.UI.Text thedudeSelectedText;
        [SerializeField] private UnityEngine.UI.Text borkSelectedText;
        [SerializeField] private UnityEngine.UI.Text sirkneeSelectedText;
        [SerializeField] private UnityEngine.UI.Text capsparklesSelectedText;

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
            //GameManager.Instance.SetLevelSelectMenu();
            GameManager.Instance.SetPlaying();

        }

        public void ReturnEvent()
        {
            Debug.Log("Return");
            GameManager.Instance.SetMainMenu();
        }

        public void SelectTheGirl()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "The Girl"));
            //GameManager.Instance.Player.ship = ;
            Debug.Log("Selected The Girl");
            UpdateSelections();
            //ships.Add(Ga)
            //ships.Add(GameManager.Instance.)
        }

        public void SelectTMIN8OR()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "T-MIN-8-OR"));
            Debug.Log("Selected T-MIN-8-OR");
            UpdateSelections();

        }

        public void SelectLeGeneral()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "Le General"));
            Debug.Log("Selected Le General");
            UpdateSelections();
        }

        public void SelectDapper()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "Dapper"));
            Debug.Log("Selected Dapper");
            UpdateSelections();
        }

        public void SelectRobin()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "Robin Williams"));
            Debug.Log("Selected Robin");
            UpdateSelections();
        }

        public void SelectTheDude()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "The Dude"));
            Debug.Log("Selected The Dude");
            UpdateSelections();
        }

        public void SelectBork()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "Bork"));
            Debug.Log("Selected Bork");
            UpdateSelections();
        }

        public void SelectSirKnee()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "Sir Knee"));
            Debug.Log("Selected Sir Knee");
            UpdateSelections();
        }

        public void SelectCapSparkles()
        {
            GameManager.Instance.Player.SetShip(GameManager.Instance.Ships.FirstOrDefault(s => s.pilot == "Cap Sparkles"));
            Debug.Log("Selected Cap Sparkles");
            UpdateSelections();

        }

        private void UpdateSelections()
        {
            ClearSelectedText();
            // Calc AI
            GameManager.Instance.RandomizeAI();
            SetSelectedText(GameManager.Instance.Player.ship.pilot, "Player", Color.white);
            for(int i=0;i<5;i++)
            {
                SetSelectedText(GameManager.Instance.AI[i].ship.pilot, "AI " + i, GameManager.Instance.AIColors[i]);
            }

        }

        private void ClearSelectedText()
        {
            thegirlSelectedText.text = "";
            tmin8orSelectedText.text = "";
            legeneralSelectedText.text = "";
            dapperSelectedText.text = "";
            robinSelectedText.text = "";
            thedudeSelectedText.text = "";
            borkSelectedText.text = "";
            sirkneeSelectedText.text = "";
            capsparklesSelectedText.text = "";
        }

        private void SetSelectedText(string character, string text, Color color)
        {
            switch (character)
            {
                case "The Girl":
                    thegirlSelectedText.text = text;
                    thegirlSelectedText.color = color;
                    break;
                case "T-MIN-8-OR":
                    tmin8orSelectedText.text = text;
                    tmin8orSelectedText.color = color;
                    break;
                case "Le General":
                    legeneralSelectedText.text = text;
                    legeneralSelectedText.color = color;
                    break;
                case "Dapper":
                    dapperSelectedText.text = text;
                    dapperSelectedText.color = color;
                    break;
                case "Robin Williams":
                    robinSelectedText.text = text;
                    robinSelectedText.color = color;
                    break;
                case "The Dude":
                    thedudeSelectedText.text = text;
                    thedudeSelectedText.color = color;
                    break;
                case "Bork":
                    borkSelectedText.text = text;
                    borkSelectedText.color = color;
                    break;
                case "Sir Knee":
                    sirkneeSelectedText.text = text;
                    sirkneeSelectedText.color = color;
                    break;
                case "Cap Sparkles":
                    capsparklesSelectedText.text = text;
                    capsparklesSelectedText.color = color;
                    break;
            }

        }
    }
}