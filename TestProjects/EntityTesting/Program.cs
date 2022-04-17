using System;
using Swordfish;
using Swordfish.Core;
using Swordfish.ECS;
using Swordfish.Scripting;
using System.Threading;
using System.Reflection;
using Swordfish.Components.UI;
using Swordfish.Components;
using Swordfish.Core.Rendering;
using OpenTK.Windowing.Common;

namespace EntityTesting
{
    class Program
    {
        public static GameState gameState = new TestGameState();
        public static Game testGame;
        static void Main(String[] args)
        {
            Entity _camera = new Entity();
            _camera.AddComponent(new Camera());
            gameState.GameScene.Entities.Add(_camera);
            createWindow();
        }
        static void createWindow()
        {
            testGame = new Game(1280, 720, "Test client", gameState);
        }
    }
}