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

namespace EntityTesting
{
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
            ContextButton button = new ContextButton(300, 100, 0, 0, "hello!");
            Entity buttonEntity = new Entity(button);
            Label label = new Label("Hello World!", 1.2F, new OpenTK.Mathematics.Vector3(0,0,0));
            Label label2 = new Label("Hello World!", 1.2F, new OpenTK.Mathematics.Vector3(0, 0, 0));
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
