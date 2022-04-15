using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;

namespace Swordfish.Components
{
    public class Label : IComponent
    {
        private TextStyle _style;
        private string _text;
        public Label(string text)
        {
            _text = text;
        }
        public Label(string text, TextStyle style)
        {
            _text = text;
            _style = style;
        }

        public void OnLoad()
        {
        }
        public void Update()
        {
        }
    }
}
