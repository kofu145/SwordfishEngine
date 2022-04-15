using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using Swordfish.Core.Rendering;

namespace Swordfish.Components.UI
{
    public class Label : IComponent
    {
        private TextStyle _style;
        public string Text;
        public float FontSize;

        public Label(string text, float fontSize)
        {
            Text = text;
            FontSize = fontSize;
        }
        public Label(string text, TextStyle style, float fontSize)
        {
            Text = text;
            _style = style;
            FontSize = fontSize;
        }

        public void OnLoad()
        {
        }
        public void Update()
        {
        }
    }
}
