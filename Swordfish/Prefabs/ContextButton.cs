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
        public ContextButton(string filepath, string title, int width, int height, int xPos, int yPos, int zPos)
        {
            string[] ButtonFilePaths = { filepath, "../../../Resources/hovered.png" };
            Transform ButtonTransform = new Transform();
            Label ButtonLabel;
            Sprite ButtonSprite = new Sprite(filepath);
            Animation ButtonSprites = new Animation(ButtonFilePaths);
            Button ButtonComponent = new Button(xPos, yPos, width, height);

            // weight font size to h, w 
            ButtonLabel = new Label(title, 1.2f, new Vector3(1, 0, 0), new Core.FontLibrary());
            ButtonTransform.Position = new Vector3((xPos - 640) + width / 2, (-yPos + 360) - height / 2, zPos);
            // crazy magic to map the button's texture to it's selected width
            ButtonTransform.Scale = new Vector2((float)width / ButtonSprite.Width, (float)height / ButtonSprite.Height);

            AddComponent(ButtonSprites).AddComponent(ButtonLabel).AddComponent(ButtonTransform).AddComponent(ButtonComponent);
            ButtonSprites.SetTexture(0);
        }
    }
}
