using System;
using System.Collections.Generic;
using System.Linq;
using Aspects.Orbit.Scripts;
using UnityEngine;

namespace Aspects.Services.Orbit
{
    public class OrbitService : IOrbitService
    {
        public event Action<OrbitChild> ChildAdded;

        private Dictionary<string, OrbitEntity> _orbitEntities = new();
        private List<OrbitChild> _children = new();
        
        public void AddChild(string orbitKey, OrbitChild child)
        {
            _children.Add(child);
            child.OrbitEntity = GetOrbitEntity(orbitKey);
            
            ChildAdded?.Invoke(child);
        }

        public OrbitChild AddChild(string orbitKey, GameObject child)
        {
            var orbitChild = new OrbitChild(child);
            AddChild(orbitKey, orbitChild);
            
            return orbitChild;
        }

        public void AddOrbit(string key, OrbitEntity orbitEntity)
            => _orbitEntities.Add(key, orbitEntity);

        public Vector3 GetOrbitCenter(string key)
            => GetOrbitEntity(key).GetCenter();

        private OrbitEntity GetOrbitEntity(string key)
            => _orbitEntities.FirstOrDefault(_ => _.Key == key).Value;

        public OrbitChild GetChild(GameObject gameObject)
            => _children.FirstOrDefault(_ => _.OrbitObject == gameObject);

        public IEnumerable<OrbitChild> GetChildren()
            => _children;
    }
}