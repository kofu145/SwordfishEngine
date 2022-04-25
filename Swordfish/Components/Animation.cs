﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using Newtonsoft.Json;

namespace Swordfish.Components
{
    public class Animation : Component
    {
        private List<Texture> textures = new List<Texture>();
        public Animation(string[] filePaths)
        {
            foreach(string i in filePaths)
            {
                textures.Add(Texture.LoadFromFile(i, TextureFilter.nearest));
            }
        }

        public void SetTexture(int textureId)
        {
            ParentEntity.AddComponent(new Sprite(textures[textureId]));
        }

        public override void OnLoad()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}