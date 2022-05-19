using Swordfish.Core;
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

        public int Priority { get; }

        public void OnLoad();

        // should be (will be) deprecated in a future release
        public void EarlyUpdate(GameTime gameTime);
        // update might want scene?
        public void Update(GameTime gameTime);

        public void LateUpdate(GameTime gameTime);

        public void SetParent(Entity parentEntity);

        public void ApplyNewCopyParent(Entity entity);

    }
}
