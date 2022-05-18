using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using OpenTK.Graphics.OpenGL4;
using Newtonsoft.Json;
using Swordfish.Core;

namespace Swordfish.Components
{
    public class Animation : Component
    {
        private List<Texture> textures = new List<Texture>();
        private int? textureId;
        public Animation(string[] filePaths)
        {
            foreach(string i in filePaths)
            {
                textures.Add(Texture.LoadFromFile(i, TextureFilter.nearest));
            }
        }

        public void SetTexture(int textureId)
        {
            // only change texture if we know the "to render" texture has changed
            if (textureId != this.textureId)
            {
                ParentEntity.AddComponent(new Sprite(textures[textureId]));
                this.textureId = textureId;
            }
        }

        public override void OnLoad()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
