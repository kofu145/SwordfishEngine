using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Mathematics;

namespace Swordfish.Core.Rendering
{
    internal class GameCamera
    {
        public Vector2 WindowSize { private get; set; }
        public float Zoom;

        public GameCamera(float zoom)
        {
            this.Zoom = zoom;
        }

        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(new Vector3(0f, 0f, 10f), new Vector3(0f, 0f, -1f), new Vector3(0f, 1f, 0f));
        }

        public Matrix4 GetProjectionMatrix()
        {
            
            return Matrix4.CreateOrthographicOffCenter
                (
                    -WindowSize.X / 2 * Zoom, WindowSize.X / 2 * Zoom, // scaled minWidth/maxWidth
                    -WindowSize.Y / 2 * Zoom, WindowSize.Y / 2 * Zoom, // scaled minHeight/maxHeight
                    -100f, 100.0f
                );
            
            //return Matrix4.CreateOrthographicOffCenter(-400f, 400f, -300f, 300f, .1f, 100f);
        }

    }
}
