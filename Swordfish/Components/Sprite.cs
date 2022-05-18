using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Graphics.OpenGL4;
using Swordfish.Core.Math;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using Swordfish.Core;

namespace Swordfish.Components
{
    public class Sprite : Component
    {
        private Texture texture;

        public int Width => this.texture.Width;
        public int Height => this.texture.Height;
        public Vector2 Origin { get; set; }
        public Vector3 Color { get; set; }

        internal Sprite(Texture texture)
        {
            this.texture = texture;
            this.Origin = new Vector2(Width / 2, Height / 2);
            this.Color = new Vector3(1f, 1f, 1f);
        }

        /// <summary>
        /// Creates a new sprite component. Origin is automatically centered.
        /// </summary>
        /// <param name="textureFilePath">The filepath to the image you are using for your sprite.</param>
        public Sprite(string textureFilePath)
        {
            this.texture = Texture.LoadFromFile(textureFilePath, TextureFilter.nearest);
            this.Origin = new Vector2(Width / 2, Height / 2);
            this.Color = new Vector3(1f, 1f, 1f);
        }

        /// <summary>
        /// Creates a new sprite component. Origin is automatically centered.
        /// </summary>
        /// <param name="textureFilePath">The filepath to the image you are using for your sprite.</param>
        /// <param name="r">The red value of the rgb color model to apply to this sprite.</param>
        /// <param name="g">The green value of the rgb color model to apply to this sprite.</param>
        /// <param name="b">The blue value of the rgb color model to apply to this sprite.</param>
        public Sprite(string textureFilePath, float r, float g, float b)
        {
            this.texture = Texture.LoadFromFile(textureFilePath, TextureFilter.nearest);
            this.Color = new Vector3(r / 255f, g / 255f, b / 255f);
            this.Origin = new Vector2(Width / 2, Height / 2);
        }

        /// <summary>
        /// Creates a new sprite component. Origin is automatically centered.
        /// </summary>
        /// <param name="textureFilePath">The filepath to the image you are using for your sprite.</param>
        /// <param name="r">The red value of the rgb color model to apply to this sprite.</param>
        /// <param name="g">The green value of the rgb color model to apply to this sprite.</param>
        /// <param name="b">The blue value of the rgb color model to apply to this sprite.</param>
        public Sprite(string textureFilePath, float r, float g, float b, Vector2 origin)
        {
            this.texture = Texture.LoadFromFile(textureFilePath, TextureFilter.nearest);
            this.Color = new Vector3(r / 255f, g / 255f, b / 255f);
            this.Origin = origin;
        }

        /// <summary>
        /// Creates a new sprite component. Origin is automatically centered.
        /// </summary>
        /// <param name="textureFilePath">The filepath to the image you are using for your sprite.</param>
        /// <param name="r">The red value of the rgb color model to apply to this sprite.</param>
        /// <param name="g">The green value of the rgb color model to apply to this sprite.</param>
        /// <param name="b">The blue value of the rgb color model to apply to this sprite.</param>
        public Sprite(string textureFilePath, float r, float g, float b, (float originX, float originY) origin)
        {
            this.texture = Texture.LoadFromFile(textureFilePath, TextureFilter.nearest);
            this.Color = new Vector3(r / 255f, g / 255f, b / 255f);
            this.Origin = new Vector2(origin.originX, origin.originY);
        }

        public override void OnLoad()
        {

        }
        public override void Update(GameTime gameTime)
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
