using System;
using System.Collections.Generic;
using Aspects.Orbit.Scripts;
using UnityEngine;

namespace Aspects.Services.Orbit
{
    public interface IOrbitService
    {
        event Action<OrbitChild> ChildAdded;

        void AddChild(string orbitKey, OrbitChild orbitChild);
        OrbitChild AddChild(string orbitKey, GameObject gameObject);
        IEnumerable<OrbitChild> GetChildren();
        OrbitChild GetChild(GameObject gameObject);
        void AddOrbit(string key, OrbitEntity orbitEntity);
        Vector3 GetOrbitCenter(string key);
    }
}