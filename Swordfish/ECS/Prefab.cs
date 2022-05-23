using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.ECS
{
    public abstract class Prefab : Entity
    {
        public Prefab() : base(Guid.NewGuid())
        {

        }

        // this is shite and it doesn't fucking work everything is bullshit lol
        /*
        public virtual Entity Instantiate()
        {
            return GetDeepCopy();
        }
        */

        public abstract Entity Instantiate();
    }
}
