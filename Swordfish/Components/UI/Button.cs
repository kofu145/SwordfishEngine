using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using OpenTK.Graphics;
using Swordfish.Components;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using Swordfish.Core.Rendering;
using Swordfish.Core;

namespace Swordfish.Components.UI
{
    ///<summary>
    /// basic context button with width, height, x position, y position and title
    ///</summary>
    public class ContextButton : Entity
    {
        public Transform ButtonTransform = new Transform();
        public Sprite ButtonSprite;
        public Sprite Hovered = new Sprite("C:\\Users\\Logan\\Pictures\\buttons\\hovered.png");
        public Label ButtonLabel;
        public int xpos;
        public int ypos;
        public int Width;
        public int Height;
        public UI_ID identifier = new UI_ID();
        public ContextButton(String filepath, String title, int width, int height, int xPos, int yPos, int zPos)
        {
            xpos = xPos;
            ypos = yPos;
            Width = width;
            Height = height;
            ButtonSprite = new Sprite(filepath);
            // weight font size to h, w 
            ButtonLabel = new Label(title, 1.2f, (1, 0, 0), new Core.FontLibrary());
            ButtonTransform.Position = ((xPos-640)+width/2, (-yPos+360)-height/2, zPos);
            // crazy magic to map the button's texture to it's selected width
            ButtonTransform.Scale = ((float)width / ButtonSprite.Width, (float)height / ButtonSprite.Height);
            this.AddComponent(ButtonSprite).AddComponent(ButtonLabel).AddComponent(ButtonTransform).AddComponent(identifier);
        }
        public void Update()
        {
            Vector2 mousePos = InputManager.Instance.GetMousePos();
            if (mousePos.X > xpos && mousePos.X < xpos + Width && mousePos.Y > ypos && mousePos.Y < ypos + Height)
            {
                this.AddComponent(Hovered);
            }
            else
            {
                this.AddComponent(ButtonSprite);
            }
           // if (mousePos.X < 
        }
    }
    public class UI_ID : IComponent
    {
        public void OnLoad()
        {
        }

        public void Update()
        {
        }
    }

}
