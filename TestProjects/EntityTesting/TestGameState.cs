using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.Core;
using Swordfish.ECS;
using Swordfish.Components.UI;
using Swordfish.Components;
using System.Drawing;
using Swordfish.ImGui;
using Swordfish.Scripting;
using Swordfish.Prefabs;
using Swordfish.Core.Math;

namespace EntityTesting
{
    public class InitGameState : GameState
    {
        public override void Draw()
        {
        }
        public override void Initialize()
        {
            Camera C = new Camera();
            this.GameScene.Entities.Add(new Entity(C));
        }
        public override void OnLoad()
        {
            // Create our prefab
            ContextButton ButtonPrefab = new ContextButton("C:\\Users\\Logan\\Pictures\\buttons\\normal.png", "1", 300, 100, 0, 0, 0);

            FontLibrary lib = new FontLibrary();
            Entity Button = ButtonPrefab.Instantiate();


 

            this.GameScene.Entities.Add(Button);

        }        
        public override void OnUnload()
        {

        }
        public override void Update()
        {
        }
    }
    public class TestGameState : GameState
    {
        public TestGameState()
        {
        }
        public override void Draw()
        {
        }
        public override void Initialize()
        {
            Console.WriteLine("Init!");
        }
        public override void OnLoad()
        {
            FontLibrary fontLibrary = new FontLibrary();
            Label label = new Label("Hello World!", 1.2F, new Vector3(0,0,0), fontLibrary);
            Label label2 = new Label("Hello World!", 1.2F, new Vector3(0, 0, 0), fontLibrary);
            Entity buttonEntity = new Entity();
            buttonEntity.AddComponent(label2);
            Entity chainTest = new Entity();
            Sprite image = new Sprite("F:\\Downloads\\IMG_20220330_181604.jpg");
            chainTest.AddComponent(new Transform()).AddComponent(label).AddComponent(image);
            Interpreter.Instance.createScript("testScript.py", new Entity[] { chainTest });
            Console.WriteLine("?" + chainTest.HasComponent<Transform>());
            this.GameScene.Entities.Add(chainTest);
            this.GameScene.Entities.Add(buttonEntity);
        }
        public override void OnUnload()
        {
        }
        public override void Update()
        {
        }
    }
}
