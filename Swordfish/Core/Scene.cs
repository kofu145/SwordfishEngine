using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Swordfish.Core
{
    /// <summary>
    /// A class encapsulating the collection of entities for a gamestate.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// The list of all entities in the scene.
        /// </summary>
        public List<Entity> Entities;
        private Texture backgroundTexture;
        public Scene()
        {
            Entities = new List<Entity>();
        }

        public void SetBackgroundImage(string filePath)
        {
            backgroundTexture = Texture.LoadFromFile(filePath, TextureFilter.linear);
        }

        internal void BindTexture(TextureUnit textureUnit)
        {
            backgroundTexture.Use(textureUnit);
        }

        internal int GetTextureHandle()
        {
            return backgroundTexture.Handle;
        }

        internal Vector2 GetBgDimensions()
        {
            return new Vector2(backgroundTexture.Width, backgroundTexture.Height);
        }

        public bool HasBackgroundImageSet()
        {
            return backgroundTexture is not null;
        }

    }
}
