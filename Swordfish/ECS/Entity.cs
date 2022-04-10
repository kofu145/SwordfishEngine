using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Swordfish.ECS
{
    public class Entity
    {
        // use arrays for cache efficiency
        private readonly ConcurrentDictionary<Type, IComponent> components;
        public readonly Guid id;
        public Entity(Guid id)
        {
            this.id = id;
            this.components = new ConcurrentDictionary<Type, IComponent>();
        }
        // class, specifies t has to be a class
        public T GetComponent<T>() where T : class, IComponent
        {
            return components.ContainsKey(typeof(T)) ? components[typeof(T)] as T : null;
        }

        public bool HasComponent<T>() where T : IComponent
        {
            return components.ContainsKey(typeof(T));
        }

        public void AddComponent<T>(T component) where T : IComponent
        {
            components[typeof(T)] = component;
        }

    }
}
