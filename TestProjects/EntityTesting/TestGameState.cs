using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swordfish.Core;
using Swordfish.ECS;
using Swordfish.Components.UI;
using Swordfish.Components;
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
            Entity chainTest = new Entity();
            Sprite image = new Sprite("C:\\Users\\Logan\\Pictures\\Capture.PNG");
            chainTest.AddComponent(image).AddComponent(new Transform());
            Console.WriteLine("?" + chainTest.HasComponent<Transform>());
            this.GameScene.Entities.Add(buttonEntity);
            this.GameScene.Entities.Add(chainTest);
        }

        public override void OnUnload()
        {
            
        }

        public override void Update()
        {
            
        }
    }
}
