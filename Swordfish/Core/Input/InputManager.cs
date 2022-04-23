using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace Swordfish.Core
{
    public class InputManager
    {
        /// <summary>
        /// The singleton instance of the InputManager.
        /// NOTE: There are some context dependent things that need to be initialized, so don't reference this
        /// until you've completely made your window (which shouldn't be a problem anyways).
        /// </summary>
        private static InputManager instance;
        private KeyboardState keyboardState;
        private MouseState mouseState;

        // Singleton Pattern Logic
        public static InputManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InputManager();
                    
                }
                return instance;
            }
        }

        // TODO: make abstractions for both things, clean this the fuck up, and possibly ECS integration?
        
        /// <summary>
        /// The current mouse position, relative to the window's 0, 0. (Top left corner)
        /// </summary>
        public Vector2 mousePos { get{ return mouseState.Position; } private set{ } }
        /// <summary>
        /// The difference in mouse position from the last frame.
        /// </summary>
        public Vector2 mouseDelta { get { return mouseState.Delta; } private set { } }
        public Vector2 prevMousePos { get { return mouseState.PreviousPosition; } private set { } }
        public Vector2 prevMouseScroll { get { return mouseState.PreviousScroll; } private set { } }
        public Vector2 scrollPos { get { return mouseState.Scroll; } private set { } }
        public Vector2 scrollDelta { get { return mouseState.ScrollDelta; } private set { } }
        

        internal void SetSystemStates(KeyboardState keyboardState, MouseState mouseState)
        {
            this.keyboardState = keyboardState;
            this.mouseState = mouseState;
        }

        public bool GetKeyDown(Keys key)
        {
            return keyboardState.IsKeyDown((OpenTK.Windowing.GraphicsLibraryFramework.Keys)key);
        }
        public bool GetKeyPressed(Keys key)
        {
            return keyboardState.IsKeyPressed((OpenTK.Windowing.GraphicsLibraryFramework.Keys)key);
        }
        public bool GetKeyUp(Keys key)
        {
            return keyboardState.IsKeyReleased((OpenTK.Windowing.GraphicsLibraryFramework.Keys)key);
        }

        public bool GetMouseButtonDown(MouseButton button){
            return mouseState.IsButtonDown(button);
        }

        public bool GetMouseButtonWasDown(MouseButton button){
            return mouseState.WasButtonDown(button);
        }

        public bool GetIsAnyMouseKeyDown(){
            return mouseState.IsAnyButtonDown;
        }

        // probably don't need these, easy enough to check for themselves, but they still here just in case.

        public bool MouseHasMoved()
        {
            return mouseState.Position != mouseState.PreviousPosition;
        }

        public bool ScrollHasMoved()
        {
            return mouseState.Scroll != mouseState.PreviousScroll;
        }

    }
}
