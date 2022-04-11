using System;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using Swordfish.Core.Rendering;

namespace Swordfish.Core
{
    public class Game
    {
        internal static Window window;
        /// <summary>
        /// Initializes the entry point for the current running game. Handles running a window which calls all update and draw methods.
        /// </summary>
        /// <param name="width">The width of the game window.</param>
        /// <param name="height">The height of the game window.</param>
        /// <param name="title">The title of the game window.</param>
        /// <param name="initialGameState">The initial game state that runs when the game opens.</param>
        public Game(int width, int height, string title, IGameState initialGameState)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(width, height),
                Title = title,
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            };

            using (window = new Window(GameWindowSettings.Default, nativeWindowSettings, initialGameState))
            {
                window.Run();
            }
        }
    }
}
