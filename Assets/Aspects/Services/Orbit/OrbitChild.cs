using Aspects.Orbit.Scripts;
using UnityEngine;

namespace Aspects.Services.Orbit
{
    public class OrbitChild
    {
        public OrbitEntity OrbitEntity { get; set; }
        public GameObject OrbitObject  { set; get; }
        public float Angle { set; get; }
        public float RotateSpeed { set; get; } = 90;

        public OrbitChild(GameObject orbitObject)
        {
            OrbitObject = orbitObject;
        }

        public bool IsAlive()
            => OrbitObject != null;
    }
}