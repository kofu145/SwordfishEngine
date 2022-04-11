using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using Swordfish.Core.Rendering;

namespace Swordfish.Components
{
    class Sprite : IComponent
    {
        internal Texture texture;
        public int Width => this.texture.Width;
        public int Height => this.texture.Height;

        public Sprite(string textureFilePath)
        {
            this.texture = Texture.LoadFromFile(textureFilePath);
        }
        public void OnLoad()
        {

        }
        public void Update()
        {

        }
    }
}
