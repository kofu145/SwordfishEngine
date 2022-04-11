using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
namespace Swordfish.UI
{
    ///<summary>
    /// Style for text that is displayed on screen.
    ///</summary>
    [Flags]
    public enum TextStyle
    {
        ITALICS = 0,
        BOLD = 1,
        STRIKETHROUGH = 2,
        UNDERLINE = 4,
    }
    internal class Text
    {

    }

    internal class Label : IComponent
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
