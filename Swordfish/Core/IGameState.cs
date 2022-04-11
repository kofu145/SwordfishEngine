using System;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.Core
{
    public interface IGameState
    {
        /// <summary>
        /// Called on the initialization of the game state.
        /// </summary>        
        void Initialize();

        /// <summary>
        /// Called on the loading of everything in the scene.
        /// </summary>
        void OnLoad();

        /// <summary>
        /// Called when content is unloaded.
        /// </summary>
        void OnUnload();

        /// <summary>
        /// Called on a fixed timestep. This should include your game logic.
        /// </summary>
        void Update();

        /// <summary>
        /// The rendering call, called before Update. Draw calls for rendering sprites should be called here.
        /// </summary>
        void Draw();
    }
}
