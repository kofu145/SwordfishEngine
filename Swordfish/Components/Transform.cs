﻿using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.Core;
using Swordfish.Core.Math;
using Swordfish.ECS;

namespace Swordfish.Components
{
    public class Transform : Component
    {
        /// <summary>
        /// A Vector2 representing position. Format is x, y, z. Z determines Z ordering.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        /// A Vector3 representing rotation. NOTE: in 2D, you should only be using the Z coordinate.
        /// </summary>
        public Vector3 Rotation;

        /// <summary>
        /// A Vector2 representing scale. Scaled by width, length.
        /// </summary>
        public Vector2 Scale;

        /// <summary>
        /// Creates a new transform component.
        /// </summary>
        public Transform()
        {
            this.Position = new Vector3(0, 0, 0);
            this.Rotation = new Vector3(0, 0, 0);
            this.Scale = new Vector2(1f, 1f);
        }

        /// <summary>
        /// Creates a new transform component.
        /// </summary>
        /// <param name="position">A Vector2 representing position.</param>
        /// <param name="rotation">A Vector3 resresenting rotation.</param>
        public Transform(Vector3 position, Vector3 rotation, Vector2 scale)
        {
            this.Position = position;
            this.Rotation = rotation;
            this.Scale = scale;
        }

        /// <summary>
        /// Creates a new transform component.
        /// </summary>
        /// <param name="x">The x value for position.</param>
        /// <param name="y">The y value for position.</param>
        /// <param name="rx">The x value for rotation.</param>
        /// <param name="ry">The y value for rotation.</param>
        /// <param name="rz">The z value for rotation.</param>
        public Transform(float x, float y, float z, float rx, float ry, float rz, float sx, float sy) :
            this(new Vector3(x, y, z), new Vector3(rx, ry, rz), new Vector2(sx, sy))
        { }

        public override void OnLoad()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }
    }
}
