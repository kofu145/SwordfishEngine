using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using Swordfish.Core.Math;

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
        private List<Entity> entities;

        private List<Entity> entitiesToAdd;
        private List<Entity> entitiesToDestroy;

        public Vector2 backgroundOffset;

        public IReadOnlyList<Entity> Entities { get { return entities; } }

        private Texture backgroundTexture;
        public Scene()
        {
            entities = new List<Entity>();
            entitiesToAdd = new List<Entity>();
            entitiesToDestroy = new List<Entity>();
            backgroundOffset = new Vector2(0, 0);
        }

        public void AddEntity(Entity entity)
        {
            entitiesToAdd.Add(entity);
        }

        public void DestroyEntity(Entity entity)
        {
            entitiesToDestroy.Add(entity);
        }

        internal void UpdateEntities()
        {
            foreach(var entity in entitiesToAdd)
            {
                entities.Add(entity);

                foreach (var component in entity.GetComponents())
                {
                    component.OnLoad();
                }

            }

            foreach(var entity in entitiesToDestroy)
            {
                foreach (var component in entity.GetComponents())
                {
                    component.OnUnload();
                }
                entities.Remove(entity);
            }

            entitiesToAdd.Clear();
            entitiesToDestroy.Clear();
        }

        public void SetBackgroundImage(string filePath)
        {
            backgroundTexture = Texture.LoadFromFile(filePath, TextureFilter.nearest);
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
