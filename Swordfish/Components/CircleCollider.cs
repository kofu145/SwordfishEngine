using System;
using System.Linq;
using Swordfish.Core;
using Swordfish.ECS;

namespace Swordfish.Components
{
    /// <summary>
    /// A circular collider that handles collisions.
    /// The collider is centered around the entity's position.
    /// </summary>
    public class CircleCollider : Component
    {
        public delegate void OnCollisionEnter(CircleCollider collidedWith);
        public delegate void DuringCollision(CircleCollider collidedWith);
        public delegate void OnCollisionExit(CircleCollider collidedWith);

        public event OnCollisionEnter OnEnterCollision;
        public event DuringCollision OnCollision;
        public event OnCollisionExit OnExitCollision;

        public float Radius;
        public bool Dynamic;
        public bool IsColliding { get; private set; }
        private bool wasColliding;

        private Transform thisTransform; // we hold this reference so we don't constantly do getcomponent

        public CircleCollider(float radius, bool isDynamic)
        {
            Radius = radius;
            // maybe we can just check if we have a rigidbody?
            Dynamic = isDynamic;
        }

        public override void OnLoad()
        {
            // assume we already have a transform, may edit htis later somewhere else so addcomp order doesn't matter
            thisTransform = ParentEntity.GetComponent<Transform>();
        }

        public override void Update(GameTime gameTime)
        {
            wasColliding = false;
            if (IsColliding)
            {
                IsColliding = false;
                wasColliding = true;
            }
            var collidableEntities = GameStateManager.Instance.GetScreen().GameScene.Entities
                .Where(e => e.HasComponent<Transform>())
                .Where(e => e.HasComponent<CircleCollider>())
                // Don't want to check collisions with self
                .Where(e => e.id != ParentEntity.id);

            foreach(Entity entity in collidableEntities)
            {
                var otherTransform = entity.GetComponent<Transform>();
                var otherCollider = entity.GetComponent<CircleCollider>();

                // basic distance formula, sqrt((x2 - x1)^2 + (y2 - y1)^2) (but sqrt is expensive, so we are just doing comparison of squared)
                float squaredDistBetweenCircles =
                    (otherTransform.Position.X - thisTransform.Position.X) * (otherTransform.Position.X - thisTransform.Position.X) +
                    (otherTransform.Position.Y - thisTransform.Position.Y) * (otherTransform.Position.Y - thisTransform.Position.Y);

                if (squaredDistBetweenCircles < (Radius + otherCollider.Radius) * (Radius + otherCollider.Radius))
                {
                    if (!wasColliding)
                    {
                        OnEnterCollision?.Invoke(otherCollider);
                    }
                        // invoke collision event
                        OnCollision?.Invoke(otherCollider);
                    IsColliding = true;

                    // TODO: swept testing for higher velocity colliders
                    // do moving circle collision (reposition circle accordingly)
                    if (Dynamic)
                    {
                        double angle = Math.Atan2(otherTransform.Position.Y - thisTransform.Position.Y, otherTransform.Position.X - thisTransform.Position.X);

                        // we only calc the dist here as needed, because sqrt() is expensive
                        var distBetweenCircles = Math.Sqrt(squaredDistBetweenCircles);

                        var distToMove = Radius + otherCollider.Radius - distBetweenCircles;

                        if (!otherCollider.Dynamic)
                        {
                            // move the collider to match bounds
                            thisTransform.Position.X -= (float)(Math.Cos(angle) * distToMove);
                            thisTransform.Position.Y -= (float)(Math.Sin(angle) * distToMove);
                            IsColliding = false;
                        }
                        else
                        {
                            // move the collider to match bounds
                            otherTransform.Position.X += (float)(Math.Cos(angle) * distToMove);
                            otherTransform.Position.Y += (float)(Math.Sin(angle) * distToMove);
                            IsColliding = false;

                        }

                    }
                    
                }
                if (!IsColliding && wasColliding)
                {
                    OnExitCollision?.Invoke(otherCollider);
                }

            }

        }

        public override void OnUnload()
        {
            OnEnterCollision = null;
            OnCollision = null;
            OnExitCollision = null;
        }

    }
}
