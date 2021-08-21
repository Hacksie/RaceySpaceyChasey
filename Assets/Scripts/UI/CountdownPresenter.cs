
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

namespace HackedDesign.UI
{
    public class CountdownPresenter : AbstractPresenter
    {
        [SerializeField] private UnityEngine.UI.Text text;
        public override void Repaint()
        {
            
        }

        public void SetText(string text)
        {
            this.text.text = text;
        }
    }
}