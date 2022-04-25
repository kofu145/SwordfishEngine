using System;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.ECS
{
    /// <summary>
    /// An interface for the contents of all components; any defined component MUST derive from this.
    /// </summary>
    public interface IComponent
    {
        public Entity ParentEntity { get; }
        public void OnLoad();

        public void Update();

    }
}
