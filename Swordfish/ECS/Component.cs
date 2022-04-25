﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swordfish.ECS
{
    public abstract class Component : IComponent
    {
        // this can be a guid, but then there is a cost because you have to search from the scene every time
        private Entity parentEntity;
        public Entity ParentEntity { get { return parentEntity; } private set { parentEntity = value; } }

        // these two methods aren't abstract in case the users don't want to define them

        public abstract void OnLoad();

        public abstract void Update();

        public void SetParent(Entity entity)
        {
            ParentEntity.AddComponent(this);
            ParentEntity = entity;
            entity.RemoveComponent(this);
        }
    }
}