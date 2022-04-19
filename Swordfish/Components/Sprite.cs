using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Swordfish.ECS;
using Swordfish.Core.Rendering;


namespace Swordfish.Components
{
    public class Sprite : IComponent
    {
        private Texture texture;

        public int Width => this.texture.Width;
        public int Height => this.texture.Height;

        public Vector3 Color { get; set; }

        /// <summary>
        /// Creates a new sprite component.
        /// </summary>
        /// <param name="textureFilePath">The filepath to the image you are using for your sprite.</param>
        public Sprite(string textureFilePath)
        {
            this.texture = Texture.LoadFromFile(textureFilePath, TextureFilter.nearest);
            this.Color = new Vector3(1f, 1f, 1f);
        }

        /// <summary>
        /// Creates a new sprite component.
        /// </summary>
        /// <param name="textureFilePath">The filepath to the image you are using for your sprite.</param>
        /// <param name="r">The red value of the rgb color model to apply to this sprite.</param>
        /// <param name="g">The green value of the rgb color model to apply to this sprite.</param>
        /// <param name="b">The blue value of the rgb color model to apply to this sprite.</param>
        public Sprite(string textureFilePath, float r, float g, float b)
        {
            this.texture = Texture.LoadFromFile(textureFilePath, TextureFilter.nearest);
            this.Color = new Vector3(r, g, b);
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
