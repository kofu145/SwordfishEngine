using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using Swordfish.Core;
using Swordfish.Core.Math;

namespace Swordfish.Components.UI
{
    public class Label : Component
    {
        private TextStyle _style;
        public string Text { get; set; }
        public float FontSize { get; set; }
        internal FontLibrary fontLibrary;

        /// <summary>
        /// Color vector for text color. Format is 255, 255, 255 rgb.
        /// </summary>
        public Vector3 Color;

        public Label(string text, float fontSize, Vector3 color, FontLibrary fontLibrary)
        {
            Text = text;
            FontSize = fontSize;
            Color = new Vector3(color.X/255f, color.Y/255f, color.Z/255f);
            this.fontLibrary = fontLibrary;
        }
        public Label(string text, float fontSize, float r, float g, float b, FontLibrary fontLibrary)
        {
            Text = text;
            FontSize = fontSize;
            Color = new Vector3(r / 255f, g / 255f, b / 255f);
            this.fontLibrary = fontLibrary;
        }

        public Label(string text, TextStyle style, float fontSize, Vector3 color, FontLibrary fontLibrary)
        {
            Text = text;
            _style = style;
            FontSize = fontSize;
            Color = new Vector3(color.X / 255f, color.Y / 255f, color.Z / 255f);
            this.fontLibrary = fontLibrary;
        }
        public Label(string text, TextStyle style, float fontSize, float r, float g, float b, FontLibrary fontLibrary)
        {
            Text = text;
            _style = style;
            FontSize = fontSize;
            Color = new Vector3(r / 255f, g / 255f, b / 255f);
            this.fontLibrary = fontLibrary;
        }


        public override void OnLoad()
        {
        }
        public override void Update(GameTime gameTime)
        {
        }
    }
}
