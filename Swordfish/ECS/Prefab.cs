using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.ECS
{
    public class Prefab : Entity
    {
        public Prefab() : base(Guid.NewGuid())
        {

        }

        public Entity Instantiate()
        {
            return GetDeepCopy();
        }

    }
}
