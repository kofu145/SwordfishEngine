using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using OpenTK.Graphics;
using Swordfish.Components;
using OpenTK.Graphics.OpenGL4;
using Swordfish.Core.Rendering;
using Swordfish.Core;
using Swordfish.Core.Math;

namespace Swordfish.Components.UI
{
    ///<summary>
    /// basic context button with width, height, x position, y position and title
    ///</summary>
    public class Button : Component
    {

        public int Width;
        public int Height;
        public Button(int width, int height)
        {
            Width = width;
            Height = height;
        }
        public override void OnLoad()
        {
        }

        public override void Update()
        {
            
            var transform = ParentEntity.GetComponent<Transform>();
            var animation = ParentEntity.GetComponent<Animation>();
            Vector2 mousePos = InputManager.Instance.mousePos;
            if (mousePos.X > transform.Position.X && mousePos.X < transform.Position.X + Width &&
                mousePos.Y > transform.Position.Y && mousePos.Y < transform.Position.Y + Height)
            {
                animation.SetTexture(1);
            }
            else
            {
                animation.SetTexture(0);
            }
           // if (mousePos.X < */
        }
    }
}
