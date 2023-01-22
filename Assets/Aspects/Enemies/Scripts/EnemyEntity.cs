using Aspects.Services.Factories.Scripts;
using DG.Tweening;
using UnityEngine;

namespace Aspects.Enemies.Scripts
{
    [RequireComponent(typeof(MovementComponent))]
    public class EnemyEntity : MonoBehaviour
    {
        private MovementComponent _movementComponent;
        
        private void Awake()
        {
            _movementComponent = GetComponent<MovementComponent>();
        }

        private void Start()
        {
            transform.localScale = Vector3.zero;
            gameObject.transform.DOScale(Vector3.one, 0.5f);
        }

        private void Update()
        {
            _movementComponent.Speed += Time.deltaTime;
            transform.right = (_movementComponent.Velocity * 1000) - transform.position;
        }

        public void SetSpeed(float speed)
            => _movementComponent.Speed = speed;

        public void SetDirection(Vector3 direction)
            => _movementComponent.Direction = direction;

        public class Factory : ComponentFactory<EnemyEntity>
        {
        }
    }
}