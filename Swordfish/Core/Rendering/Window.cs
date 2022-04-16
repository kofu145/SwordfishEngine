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
using Swordfish.ImGui;
using ImGuiNET;
using Swordfish.Core.Rendering.Renderers;
using Swordfish.Core.Input;
using Swordfish.Scripting;

namespace Swordfish.Core.Rendering
{
    internal class Window : GameWindow
    {
        ImGuiController imGuiRenderer;

        private SpriteRenderer spriteRenderer;

        private CharTexture charTexture;

        private TextRenderer textRenderer;

        private Camera cameraComponent;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, IGameState initialGameState)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            GameStateManager.Instance.AddScreen(initialGameState);
            InputManager.Instance.SetSystemStates(KeyboardState, MouseState);
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);

            imGuiRenderer = new ImGuiController(Size.X, Size.Y);
            this.spriteRenderer = new SpriteRenderer(Size.X, Size.Y);
            this.charTexture = new CharTexture(TextureUnit.Texture0);
            this.textRenderer = new TextRenderer();
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

            imGuiRenderer.Update(this, (float)e.Time);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            var currentScene = GameStateManager.Instance.GetScreen().GameScene;
            spriteRenderer.Draw(currentScene, cameraComponent.gameCamera);
            textRenderer.Render(currentScene, charTexture, cameraComponent.gameCamera);

            ImGuiNET.ImGui.ShowDemoWindow();
            imGuiRenderer.Render();
            ImGuiUtil.CheckGLError("End of frame");
            Interpreter.Instance.Update();

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

        protected override void OnTextInput(TextInputEventArgs e)
        {
            base.OnTextInput(e);
            imGuiRenderer.PressChar((char)e.Unicode);
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            imGuiRenderer.MouseScroll(e.Offset);
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Size.X, Size.Y);

            if (cameraComponent.AutoSetCameraSize)
                cameraComponent.SetCameraBounds(Size.X, Size.Y);
            imGuiRenderer.WindowResized(ClientSize.X, ClientSize.Y);

        }

        protected override void OnUnload()
        {
            base.OnUnload();
            // Unbind all the resources by binding the targets to 0/null.
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
            GL.BindVertexArray(0);
            GL.UseProgram(0);

            spriteRenderer.Dispose();
            
            GameStateManager.Instance.OnUnload();
        }

    }
}
