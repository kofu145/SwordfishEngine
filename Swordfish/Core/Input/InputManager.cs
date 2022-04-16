using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;

namespace Swordfish.Core
{
    class InputManager
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


    }
}
