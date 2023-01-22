using System;
using Aspects.Services.Factories.Scripts;
using Aspects.Services.InputService;
using Aspects.Services.Orbit;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Aspects.Player.Scripts
{
    public class PlayerEntity : MonoBehaviour
    {
        private IInputService _inputService;
        private IOrbitService _orbitService;

        private OrbitChild OrbitChild => _orbitService.GetChild(gameObject);

        [Inject]
        private void Construct(
            IInputService inputService,
            IOrbitService orbitService)
        {
            _inputService = inputService;
            _orbitService = orbitService;

            _inputService.SpaceClicked += OnSpaceClicked;
        }

        private void Update()
        {
            var dir = OrbitChild.OrbitEntity.GetCenter() - transform.position;
            dir.z = 0;
            
            transform.right = dir;
        }

        private void OnSpaceClicked()
            => OrbitChild.RotateSpeed *= -1;

        public class Factory : ComponentFactory<PlayerEntity>
        {
        }
    }
}