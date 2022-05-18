using System;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.Core
{
    /// <summary>
    /// A convenient interface for all gamestates. This should NOT be utilized for creating gamestates.
    /// </summary>
    /// <remarks>
    /// This should only be utilized as a check for any type of gamestate, rather
    /// than for the implementation of one.
    /// </remarks>
    public interface IGameState
    {
        /// <summary>
        /// The main scene of the game state.
        /// </summary>
        Scene GameScene { get; }

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
        void Update(GameTime gameTime);

        /// <summary>
        /// The rendering call, called before Update. Draw calls for rendering sprites should be called here.
        /// </summary>
        void Draw();
    }
}
