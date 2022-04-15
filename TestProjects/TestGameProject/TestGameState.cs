using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.Core;
using Swordfish.ECS;
using Swordfish.Components;
using Swordfish.Components.UI;

namespace TestGameProject
{
    class TestGameState : GameState
    {


        public override void Initialize()
        {
            var cameraEntity = new Entity();
            cameraEntity.AddComponent(new Camera());
            cameraEntity.AddComponent(new Transform());
            this.GameScene.Entities.Add(cameraEntity);

        }

        public override void OnLoad()
        {
            var testEntity = new Entity();
            testEntity.AddComponent(new Sprite("../../../Resources/fubuki.png"));
            testEntity.AddComponent(new Transform());
            this.GameScene.Entities.Add(testEntity);

            var textTestEntity = new Entity();
            textTestEntity.AddComponent(new Label("Hello         World", 1f));
            textTestEntity.AddComponent(new Transform());
            this.GameScene.Entities.Add(textTestEntity);
        }

        public override void OnUnload()
        {
            
        }

        public override void Draw()
        {

        }

        public override void Update()
        {
            
        }
    }
}
