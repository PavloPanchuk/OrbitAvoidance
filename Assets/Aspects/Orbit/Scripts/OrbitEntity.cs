using Aspects.Services.Factories.Scripts;
using Aspects.Services.Orbit;
using UnityEngine;
using Zenject;

namespace Aspects.Orbit.Scripts
{
    public class OrbitEntity : MonoBehaviour
    {
        [SerializeField] private float radius;

        public float Radius { get => radius; set => radius = value; }
        private Vector3 OrbitCenter => transform.position;

        private IOrbitService _orbitService;

        [Inject]
        public void Construct(IOrbitService orbitService)
        {
            _orbitService = orbitService;
        }

        private void Update()
        {
            foreach (var orbitChild in _orbitService.GetChildren())
                RotateOrbitChild(orbitChild);
        }

        public Vector3 GetCenter()
            => transform.position;

        private void RotateOrbitChild(OrbitChild orbitChild)
        {
            if(orbitChild == null || !orbitChild.IsAlive())
                return;
            
            var orbitObject = orbitChild.OrbitObject;
            var angleRad = orbitChild.Angle * Mathf.Deg2Rad;
            Vector3 addVector;
            
            orbitChild.Angle += orbitChild.RotateSpeed * Time.deltaTime;
            addVector = new Vector3(
                Mathf.Cos(angleRad) * radius,
                Mathf.Sin(angleRad) * radius);
                
            orbitObject.transform.position = OrbitCenter + addVector;
        }
        
        public class Factory : ComponentFactory<OrbitEntity>
        {
        }
    }
}