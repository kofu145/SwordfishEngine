using System;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.Core
{
    class GameStateManager
    {
        /// <summary>
        /// The singleton instance of the GameStateManager.
        /// </summary>
        private static GameStateManager instance;
        private Stack<IGameState> screens = new Stack<IGameState>();
        // Singleton Pattern Logic
        public static GameStateManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameStateManager();
                }
                return instance;
            }
        }
        /// <summary>
        /// Adds a screen to the top of the stack.
        /// </summary>
        /// <param name="screen">The GameState to push to the top of the stack.</param>
        public void AddScreen(IGameState screen)
        {
            try
            {
                // add screen to the stack
                screens.Push(screen);
                /*
                if (content != null)
                {
                    screens.Peek().LoadContent(content);
                }*/
                screens.Peek().Initialize();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        /// <summary>
        /// Returns the current rendering screen at the top of the stack.
        /// </summary>
        /// <returns></returns>
        public IGameState GetScreen()
        {
            try
            {
                return screens.Peek();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
        /// <summary>
        /// Removes the top screen (gamestate) from the stack.
        /// </summary>
        public void RemoveScreen()
        {
            if (screens.Count > 0)
            {
                try
                {
                    // var screen = screens.Peek();
                    screens.Pop();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        /// <summary>
        /// Clears the entire stack of gamestates.
        /// </summary>
        public void ClearScreens()
        {
            screens.Clear();
        }
        /// <summary>
        /// Purges all screens from the stack, and adds a new one.
        /// </summary>
        /// <param name="screen">The new <see cref="IGameState"/> screen to add.</param>
        public void ChangeScreen(IGameState screen)
        {
            ClearScreens();
            AddScreen(screen);
        }
        /// <summary>
        /// Updates the top screen of the stack.
        /// </summary>
        public void Update()
        {
            if (screens.Count > 0)
            {
                screens.Peek().Update();

            }
        }
        /// <summary>
        /// Renders the top screen of the stack.
        /// </summary>
        public void Draw()
        {
            if (screens.Count > 0)
            {
                screens.Peek().Draw();
            }
        }
        /// <summary>
        /// Calls OnLoad methods for all screens when loading.
        /// </summary>
        public void OnLoad()
        {
            foreach (IGameState state in screens)
            {
                state.OnLoad();
            }
        }
        /// <summary>
        /// Calls OnUnload methods for all screens when unloading.
        /// </summary>
        public void OnUnload()
        {
            foreach (IGameState state in screens)
            {
                state.OnUnload();
            }
        }
    }
}
