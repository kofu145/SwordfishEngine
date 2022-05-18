using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using OpenTK.Graphics;
using Swordfish.Components;
using OpenTK.Graphics.OpenGL4;
using Swordfish.Core.Rendering;
using Swordfish.Core;
using Swordfish.Core.Input;
using Swordfish.Core.Math;

namespace Swordfish.Components.UI

    // todo: rework a lot of this with pure references to comps in order to allow for easy access w/out prefab
{
    ///<summary>
    /// basic context button with width, height, x position, y position and title
    ///</summary>
    public class Button : Component
    {
        public event OnPress OnButtonDown;
        public event OnDepress OnButtonUp;

        public event OnStartHover OnHover;
        public event OnEndHover OnStopHover;

        public delegate void OnPress();
        public delegate void OnDepress();
        public delegate void OnStartHover();
        public delegate void OnEndHover();

        public bool isHovered = false;
        public bool isPressed = false;
        public int Width;
        public int Height;
        public int xPos;
        public int yPos;
        public Button(int width, int height, int x, int y)
        {
            Width = width;
            Height = height;
            xPos = x;
            yPos = y;
        }
        public override void OnLoad()
        {
        }
        public override void Update(GameTime gameTime)
        {
            Vector2 mousePos = InputManager.Instance.mousePos;
            // check if mouse in bounds
            if (mousePos.X > xPos && mousePos.X < xPos + Width &&
                mousePos.Y > yPos && mousePos.Y < yPos + Height)
            {
                // set to hovered if in bounds and not already hovered
                if (!isHovered)
                {
                    isHovered = true;
                    OnHover?.Invoke();
                    //StartHover();
                }
                // check if mouse button is down
                if (InputManager.Instance.GetMouseButtonDown(MouseButton.Left))
                {
                    // if is down and not pressed = false then call mousepressed
                    if (!isPressed)
                    {
                    isPressed = true;
                    OnButtonDown?.Invoke();
                    //StartPress();
                    }

                }
            }
            // if mouse is not in bounds
            else
            {
                // set hovered to false if it is true
                if (isHovered)
                {
                    isHovered = false;
                    OnStopHover?.Invoke();
                    //StopHover();
                }
            }
            // check if mouse is not pressed 
            if (!InputManager.Instance.GetMouseButtonDown(MouseButton.Left))
            {
                // if not pressed and flag is currently set true then call not pressed function
                if (isPressed)
                {
                    isPressed = false;
                    OnButtonUp?.Invoke();
                    //StopPress();
                } 
            }
        }
        // made these private, because I don't see many reasons you'd force them to trigger?
        // If you do, sounds like bad architectural design on user end

        private void StartHover()
        {
            var animation = ParentEntity.GetComponent<Animation>();
            animation.SetTexture(1);
        }
        private void StopHover()
        {
            var animation = ParentEntity.GetComponent<Animation>();
            animation.SetTexture(0);
        }
        private void StartPress()
        {
            var animation = ParentEntity.GetComponent<Animation>();
            animation.SetTexture(2);

        }
        private void StopPress()
        {
            var animation = ParentEntity.GetComponent<Animation>();
            if (isHovered)
            {
                animation.SetTexture(1);
            }
            else
            {
                animation.SetTexture(0);
            }
        }
    }
}
