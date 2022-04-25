﻿using System;
using System.Collections.Generic;
using System.Text;
using Swordfish.Core;
using Swordfish.ECS;
using Swordfish.Components;
using Swordfish.Components.UI;
using Swordfish.Core.Input;

namespace TestGameProject
{
    class TestGameState : GameState
    {

        private Entity testEntity;
        private FontLibrary arial;

        public override void Initialize()
        {
            var cameraEntity = new Entity();
            cameraEntity.AddComponent(new Camera());
            cameraEntity.AddComponent(new Transform());
            this.GameScene.Entities.Add(cameraEntity);

            arial = new FontLibrary();

        }

        public override void OnLoad()
        {
            GameScene.SetBackgroundImage("../../../Resources/washington.png");

            Prefab testPrefab = new Prefab();
            string[] thing = { "../../../Resources/towa.png" };
            testPrefab.AddComponent(new Animation(thing));
            testPrefab.AddComponent(new Transform(0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f));
            testPrefab.GetComponent<Animation>().SetTexture(0);
            this.GameScene.Entities.Add(testPrefab.Instantiate());


            testEntity = new Entity();
            testEntity.AddComponent(new Sprite("../../../Resources/fubuki.png", 255f, 255f, 255f));
            testEntity.AddComponent(new Transform(0f, 0f, 0f, 0f, 0f, 0f, 1f, 1f));
            testEntity.AddComponent(new Sound("../../../Resources/test.wav", .5f));
            this.GameScene.Entities.Add(testEntity);
            testEntity.GetComponent<Sound>().Play();

            var trans = new Transform(-250f, 0f, 0f, 0f, 180f, 10f, .3f, .3f);
            var towaEntity = new Entity();
            towaEntity
                .AddComponent(new Sprite("../../../Resources/towa.png"))
                .AddComponent(trans);
            this.GameScene.Entities.Add(towaEntity);

            towaEntity.RemoveComponent(trans);

            var towaEntity2 = new Entity();
            towaEntity2
                .AddComponent(new Sprite("../../../Resources/towa.png"))
                .AddComponent(new Transform(250f, 0f, 1f, 0f, 0f, -10f, .3f, .3f));
            this.GameScene.Entities.Add(towaEntity2);

            var osuEntity = new Entity();
            osuEntity
                .AddComponent(new Sprite("../../../Resources/osu.png"))
                .AddComponent(new Transform(0f, -200f, -.5f, 0f, 0f, 0f, .1f, .1f));
            this.GameScene.Entities.Add(osuEntity);
            

            var textTestEntity = new Entity();
            textTestEntity.AddComponent(new Label("WEEEEEEEEEEEEEEE", 1f, 0f, 50f, 0f, arial));
            textTestEntity.AddComponent(new Transform(0f, 200f, 0f, 0f, 0f, 0f, 1f, 1f));
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
