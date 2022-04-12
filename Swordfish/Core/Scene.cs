using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;

namespace Swordfish.Core
{
    /// <summary>
    /// A class encapsulating the collection of entities for a gamestate.
    /// </summary>
    public class Scene
    {
        /// <summary>
        /// The list of all entities in the scene.
        /// </summary>
        public List<Entity> Entities;

        public Scene()
        {
            Entities = new List<Entity>();
        }


    }
}
