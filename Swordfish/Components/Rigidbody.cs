using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.Core;
using Swordfish.ECS;
using Swordfish.Core.Math;

namespace Swordfish.Components
{
    public class Rigidbody : Component
    {
        public Vector2 Velocity;
        public float Mass;
        private Transform transform;

        public Rigidbody(float mass=1f)
        {
            Velocity = new Vector2(0, 0);
            Mass = mass;
        }

        public override void OnLoad()
        {
            transform = ParentEntity.GetComponent<Transform>();
        }

        public override void EarlyUpdate(GameTime gameTime)
        {
            transform.Position += new Vector3(Velocity.X, Velocity.Y, 0) * (float)gameTime.deltaTime.TotalSeconds;
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
