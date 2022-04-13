using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using Swordfish.ECS;
using Swordfish.Core.Rendering;

namespace Swordfish.Components
{
    public class Sprite : IComponent
    {
        private Texture texture;

        public int Width => this.texture.Width;
        public int Height => this.texture.Height;

        /// <summary>
        /// Creates a new sprite component.
        /// </summary>
        /// <param name="textureFilePath">The filepath to the image you are using for your sprite.</param>
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

        internal void BindTexture(TextureUnit textureUnit)
        {
            this.texture.Use(textureUnit);
        }

        internal int GetTextureHandle()
        {
            return this.texture.Handle;
        }

    }
}
