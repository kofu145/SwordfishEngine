using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.Core.Rendering;
using OpenTK.Graphics.OpenGL4;

namespace Swordfish.Core
{
    public class FontLibrary
    {
        internal FontTexture fontTexture;

        public FontLibrary(string fontFilePath = "C:/Windows/Fonts/arial.ttf")
        {
            this.fontTexture = new FontTexture(TextureUnit.Texture0, fontFilePath);
        }
    }
}
