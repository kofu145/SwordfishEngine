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







/*
 * 
 *  OLD TEST CLIENT
 *  
namespace TestGameProject
{
    // this is used for internal monitoring of the engine
    class Program
    {
        public static Scene testScene = new Scene();
        static void Main(string[] args)
        {
           



            //
            // ENTITY TESTING
            //

            // create random entities with buttons inside for test scene
            for(int i=0; i < 10; i++)
            {
                // create entity
                Entity x = new Entity(Guid.NewGuid());
                // give entity rand amount of components
                for (int z = 0; z < new Random().Next(2); z++) {
                    x.AddComponent(new ContextButton(new Random().Next(1000), new Random().Next(1000), new Random().Next(1000), new Random().Next(1000), "ButtonNum" + z));
                }
                // add entity to scene stack
                testScene.Entities.Add(x);
            }




            // Threads for different engine components

            Thread windowThread = new Thread(createWindow);
            Thread pyThread = new Thread(createPythonInstance);
            Thread monitoringThread = new Thread(monitorGlobalEnts);
            monitoringThread.Start();
            monitoringThread.Name = "Ent monitoring";
            windowThread.Start();
            windowThread.Name = "OpenGL Window";
            pyThread.Start();
            pyThread.Name = "Python interpreter";
            //temporarily manually create interpreter until engine can do it for us
            
        


        }


        public static void monitorGlobalEnts()
        {
            while (true)
            {
                Console.Clear();
                Console.Write("Ent Guid                |_x of button\n");
                foreach (Entity x in testScene.Entities)
                {
                    Console.Write("|" + x.id + "|" + (x.HasComponent<ContextButton>() ? x.GetComponent<ContextButton>()._x.ToString() : "no button") + "|\n");
                    
                }
                Console.Write("|____________________________________|_|");
                Thread.Sleep(2600);
                
                for (int i = 0; i < 10; i++)
                {
                    // create entity
                    Entity x = new Entity(Guid.NewGuid());
                    // give entity rand amount of components
                    for (int z = 0; z < new Random().Next(2); z++)
                    {
                        x.AddComponent(new ContextButton(new Random().Next(1000), new Random().Next(1000), new Random().Next(1000), new Random().Next(1000), "ButtonNum" + z));
                    }
                    // add entity to scene stack
                    testScene.Entities.Add(x);
                }
            }
        }

        public static void createPythonInstance()
        { 
            Interpreter py = new Interpreter();
            py.createScript("testScript.py");
            while (true)
            {
                py.Update();
            }

        }

        public static void createWindow()
        {

            Swordfish.Core.Game testingWindow = new Swordfish.Core.Game(1280, 720, "Hello world!", new testGameState());

        }
    }

   
}

*/
