using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;
using Swordfish.ECS;

namespace Swordfish.Components
{
    class Transform : IComponent
    {
        /// <summary>
        /// A Vector2 representing position. Format is x, y.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// A Vector3 representing rotation. NOTE: in 2D, you should only be using the Z coordinate.
        /// </summary>
        public Vector3 Rotation;

        /// <summary>
        /// Creates a new transform component.
        /// </summary>
        public Transform()
        {
            this.Position = new Vector2(0, 0);
            this.Rotation = new Vector3(0, 0, 0);
        }

        /// <summary>
        /// Creates a new transform component.
        /// </summary>
        /// <param name="position">A Vector2 representing position.</param>
        /// <param name="rotation">A Vector3 resresenting rotation.</param>
        public Transform(Vector2 position, Vector3 rotation)
        {
            this.Position = position;
            this.Rotation = rotation;
        }

        /// <summary>
        /// Creates a new transform component.
        /// </summary>
        /// <param name="x">The x value for position.</param>
        /// <param name="y">The y value for position.</param>
        /// <param name="rx">The x value for rotation.</param>
        /// <param name="ry">The y value for rotation.</param>
        /// <param name="rz">The z value for rotation.</param>
        public Transform(float x, float y, float rx, float ry, float rz) :
            this(new Vector2(x, y), new Vector3(rx, ry, rz))
        { }

        public void OnLoad()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
