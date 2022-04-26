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
        public static Game testGame;
        public static GameState gameState = new InitGameState();
        static void Main(String[] args)
        {
            createWindow();
        }
        static void createWindow()
        {
            testGame = new Game(1280, 720, "Test client", new TestGameState());
        }
    }
}