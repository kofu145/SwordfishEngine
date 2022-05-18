using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.ECS;
using Swordfish.Core.Rendering;
using Swordfish.Core.Math;
using Swordfish.Core;

namespace Swordfish.Components
{
    public class Camera : Component
    {
        internal GameCamera gameCamera;
        public bool AutoSetCameraSize;
        public float Zoom;
        /// <summary>
        /// Creates a new camera component.
        /// </summary>
        /// <remarks>
        /// You should always have at least one entity with a camera component per gamestate.
        /// </remarks>
        /// <param name="autoSetCameraSize">An optional argument specifying whether or not
        /// you want the camera to automatically set its own bounds for you.
        /// This should be left alone, unless you know what you're doing.</param>
        /// <param name="zoom">The zoom of the camera.</param>
        public Camera(bool autoSetCameraSize = true, float zoom = 1f)
        {
            this.AutoSetCameraSize = autoSetCameraSize;
            gameCamera = new GameCamera(zoom);
            this.Zoom = zoom;
        }

        /// <summary>
        /// Sets the bounds of the orthographic projection of the camera.
        /// </summary>
        /// <param name="x">Width of the window.</param>
        /// <param name="y">Height of the window.</param>
        public void SetCameraBounds(float x, float y)
        {
            gameCamera.WindowSize = new Vector2(x, y);
        }

        public override void OnLoad()
        {
        }

        public override void Update(GameTime gameTime)
        {
        }

    }
}
