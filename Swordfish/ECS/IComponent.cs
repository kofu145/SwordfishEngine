using System;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.ECS
{
    public interface IComponent
    {
        public void OnLoad();

        public void Update();

    }
}
