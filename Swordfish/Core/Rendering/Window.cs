using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using OpenTK.Windowing.Desktop;
using Swordfish.ECS;
using Swordfish.Components;

namespace Swordfish.Core.Rendering
{
    internal class Window : GameWindow
    {

        private readonly float[] quadVertices =
        {
            // Position         Texture coordinates
             1.0f, 1.0f, 0.0f, 1.0f, 1.0f, // top right
             1.0f, 0.0f, 0.0f, 1.0f, 0.0f, // bottom right
             0.0f, 0.0f, 0.0f, 0.0f, 0.0f, // bottom left
             0.0f, 1.0f, 0.0f, 0.0f, 1.0f  // top left
        };

        private readonly uint[] indices =
        {
            // Note that indices start at 0!
            0, 1, 3, // The first triangle will be the bottom-right half of the triangle
            1, 2, 3  // Then the second will be the top-right half of the triangle
        };

        private int vertexBufferObject;

        private int vertexArrayObject;

        private Shader shader;

        private Camera cameraComponent;

        private SpriteRenderer spriteRenderer;
         
        private int elementBufferObject;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, IGameState initialGameState)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            GameStateManager.Instance.AddScreen(initialGameState);
        }

        protected override void OnLoad()
        {
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(vertexArrayObject);

            vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, quadVertices.Length * sizeof(float), quadVertices, BufferUsageHint.StaticDraw);

            elementBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObject);
            GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices, BufferUsageHint.StaticDraw);

            shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");

            shader.Use();

            var vertexLocation = shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 5 * sizeof(float), 0);

            var texCoordLocation = shader.GetAttribLocation("aTexCoord");
            GL.EnableVertexAttribArray(texCoordLocation);
            GL.VertexAttribPointer(texCoordLocation, 2, VertexAttribPointerType.Float, false, 5 * sizeof(float), 3 * sizeof(float));

            // get our camera
            cameraComponent = GameStateManager.Instance.GetScreen().GameScene.Entities
                .Where(e => e.HasComponent<Camera>()).First().GetComponent<Camera>();

            if (cameraComponent.AutoSetCameraSize)
            {
                cameraComponent.SetCameraBounds(Size.X, Size.Y);
            }

            GameStateManager.Instance.OnLoad();

        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            var currentScene = GameStateManager.Instance.GetScreen().GameScene;
            spriteRenderer.Draw(currentScene, vertexArrayObject, shader, cameraComponent.gameCamera, indices.Length);

            GameStateManager.Instance.Draw();

            SwapBuffers();
            

        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // for all entities run update
            // for all scripts run update
            base.OnUpdateFrame(e);
            GameStateManager.Instance.Update();
            

        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);

            if (cameraComponent.AutoSetCameraSize)
                cameraComponent.SetCameraBounds(Size.X, Size.Y);
            
        }

        protected override void OnUnload()
        {
            base.OnUnload();
            GameStateManager.Instance.OnUnload();
        }

    }
}
