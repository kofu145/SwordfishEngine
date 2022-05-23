using System;
using System.Collections.Generic;
using Swordfish.ECS;
using Swordfish.Components;
using Swordfish.Components.UI;
using Swordfish.Core.Math;

namespace Swordfish.Prefabs
{
    public class ContextButton : Prefab
    {
        public Transform ButtonTransform;
        public Label ButtonLabel;
        public Animation ButtonSprites;
        public Button ButtonComponent;
        string[] filepath;
        private string title;
        private int width;
        private int height;
        private int xPos;
        private int yPos;
        private int zPos;

        public ContextButton(string[] filepath, string title, int width, int height, int xPos, int yPos, int zPos)
        {
            string[] ButtonFilePaths = filepath;
            ButtonTransform = new Transform();
            ButtonSprites = new Animation(ButtonFilePaths);
            ButtonComponent = new Button(width, height, xPos, yPos);
            this.filepath = filepath;
            this.title = title;
            this.width = width;
            this.height = height;
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = zPos;
            
        }

        public override Entity Instantiate()
        {
            var ButtonSprite = new Sprite(filepath[0]);

            // weight font size to h, w 
            ButtonLabel = new Label(title, .7f, new Vector3(255f, 255f, 255f), new Core.FontLibrary("./Resources/PressStart2P.ttf"));
            ButtonTransform.Position = new Vector3((xPos - 300) + width / 2, (-yPos + 400) - height / 2, zPos);
            // crazy magic to map the button's texture to it's selected width
            ButtonTransform.Scale = new Vector2((float)width / ButtonSprite.Width, (float)height / ButtonSprite.Height);

            var entity = new Entity().AddComponent(ButtonSprites).AddComponent(ButtonLabel).AddComponent(ButtonTransform).AddComponent(ButtonComponent);
            return entity;
            ButtonSprites.SetTexture(0);
        }
    }
}
