using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using OpenTK.Mathematics;

namespace Swordfish.Components.UI
{
    public class Label : IComponent
    {
        private TextStyle _style;
        public string Text;
        public float FontSize;
        /// <summary>
        /// Color vector for text color. Format is 255, 255, 255 rgb.
        /// </summary>
        public Vector3 Color;

        public Label(string text, float fontSize, Vector3 color)
        {
            Text = text;
            FontSize = fontSize;
            Color = new Vector3(color.X/255f, color.Y/255f, color.Z/255f);
        }
        public Label(string text, float fontSize, float r, float g, float b)
        {
            Text = text;
            FontSize = fontSize;
            Color = new Vector3(r / 255f, g / 255f, b / 255f);
        }

        public Label(string text, TextStyle style, float fontSize, Vector3 color)
        {
            Text = text;
            _style = style;
            FontSize = fontSize;
            Color = new Vector3(color.X / 255f, color.Y / 255f, color.Z / 255f);
        }
        public Label(string text, TextStyle style, float fontSize, float r, float g, float b)
        {
            Text = text;
            _style = style;
            FontSize = fontSize;
            Color = new Vector3(r / 255f, g / 255f, b / 255f);
        }


        public void OnLoad()
        {
        }
        public void Update()
        {
        }
    }
}
