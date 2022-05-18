using System;
using System.Threading;
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
using Swordfish.Core.Audio;
using Swordfish.Components.UI;
namespace Swordfish.Core.Rendering
{
    internal class Window : GameWindow
    {
        Thread pyThread;
        ImGuiController imGuiRenderer;
        private SpriteRenderer spriteRenderer;
        private TextRenderer textRenderer;
        private BackgroundRenderer backgroundRenderer;
        private Camera cameraComponent;
        private GameTime gameTime;

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings, IGameState initialGameState)
            : base(gameWindowSettings, nativeWindowSettings)
        {
            GameStateManager.Instance.AddScreen(initialGameState);
            InputManager.Instance.SetSystemStates(KeyboardState, MouseState);
            AudioManager.Instance.Initialize(64, .2f);
            gameTime = new GameTime(0, 0);
        }
        protected override void OnLoad()
        {
            pyThread = new Thread(Interpreter.Instance.Update);
            pyThread.Start();
            base.OnLoad();
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            imGuiRenderer = new ImGuiController(Size.X, Size.Y);
            this.backgroundRenderer = new BackgroundRenderer(Size.X, Size.Y);
            this.spriteRenderer = new SpriteRenderer(Size.X, Size.Y);
            this.textRenderer = new TextRenderer();
            // get our camera
            cameraComponent = GameStateManager.Instance.GetScreen().GameScene.Entities
                .Where(e => e.HasComponent<Camera>()).First().GetComponent<Camera>();

            if (cameraComponent.AutoSetCameraSize)
            {
                cameraComponent.SetCameraBounds(Size.X, Size.Y);
            }
            GameStateManager.Instance.OnLoad();
            foreach (var entity in GameStateManager.Instance.GetScreen().GameScene.Entities)
            {
                foreach (var component in entity.GetComponents())
                {
                    component.OnLoad();
                }
            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            imGuiRenderer.Update(this, (float)e.Time);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);
            var currentScene = GameStateManager.Instance.GetScreen().GameScene;
            //spriteRenderer.RenderBackground(currentScene, cameraComponent.gameCamera, Size.X, Size.Y);
            backgroundRenderer.Render(currentScene, cameraComponent.gameCamera, Size.X, Size.Y);        
            spriteRenderer.Draw(currentScene, cameraComponent.gameCamera);
            textRenderer.Render(currentScene, cameraComponent.gameCamera);
            ImGuiNET.ImGui.ShowDemoWindow();
            imGuiRenderer.Render();
            ImGuiUtil.CheckGLError("End of frame");
            Interpreter.UpdateHandle.Set();
            GameStateManager.Instance.Draw();
            SwapBuffers();
            
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            // for all entities run update
            // for all scripts run update
            base.OnUpdateFrame(e);
            gameTime.UpdateTime(e.Time);
            var currentScene = GameStateManager.Instance.GetScreen().GameScene;
            GameStateManager.Instance.Update(gameTime);
            foreach (var entity in GameStateManager.Instance.GetScreen().GameScene.Entities)
            {
                foreach (var component in entity.GetComponents())
                {
                    component.Update(gameTime);
                }
            }
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
            spriteRenderer.OnUnload();

            AudioManager.Instance.OnUnload();
            GameStateManager.Instance.OnUnload();
        }

    }
}
