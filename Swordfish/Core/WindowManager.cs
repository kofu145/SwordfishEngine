using System;
using System.Collections.Generic;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Swordfish.Core.Rendering;
using Swordfish.Core.Math;

namespace Swordfish.Core
{
    public class WindowManager
    {
        /// <summary>
        /// The singleton instance of the WindowManager.
        /// </summary>
        private static WindowManager instance;

        private Game game;

        /// <summary>
        /// Whether or not VSync is turned on. On by default. Set using SetVSync.
        /// </summary>
        public bool VSync { get { return Game.window.VSync == VSyncMode.On ? true : false; } private set { } }

        /// <summary>
        /// The window size, as a Vector2.
        /// </summary>
        public Vector2 Bounds { get { return new Vector2(Game.window.Size.X, Game.window.Size.Y); } private set { } }

        // Singleton Pattern Logic
        public static WindowManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WindowManager();
                }
                return instance;
            }
        }

        public void SetVSync(bool vSync)
        {
            if (VSync)
                Game.window.VSync = VSyncMode.On;
            else
                Game.window.VSync = VSyncMode.Off;
        }

        public void SetNonResizable()
        {
            Game.window.WindowBorder = WindowBorder.Fixed;
            //game.window.WindowBorder = WindowBorder.Hidden;
        }

        internal void SetGame(Game game)
        {
            this.game = game;
        }


    }
}
