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

        private Entity testEntity;
        public override void Initialize()
        {
            var cameraEntity = new Entity();
            cameraEntity.AddComponent(new Camera());
            cameraEntity.AddComponent(new Transform());
            this.GameScene.Entities.Add(cameraEntity);

        }

        public override void OnLoad()
        {
            testEntity = new Entity();
            testEntity.AddComponent(new Sprite("../../../Resources/fubuki.png"));
            testEntity.AddComponent(new Transform());
            this.GameScene.Entities.Add(testEntity);

            var textTestEntity = new Entity();
            textTestEntity.AddComponent(new Label("WEEEEEEEEEEEEEEE", 1f, 255f, 255f, 255f));
            textTestEntity.AddComponent(new Transform(0f, 150f, 0f, 0f, 0f, 1f, 1f));
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
            testEntity.GetComponent<Transform>().Rotation.Z+= 5f;
        }
    }
}
