using UnityEngine;

namespace Aspects
{
    public class MovementComponent : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private Vector3 direction;
        
        public float Speed { get => speed; set => speed = value; }
        public Vector3 Direction { get => direction; set => direction = value; }
        public Vector3 Velocity => speed * direction;

        private void Update()
        {
            if (!enabled)
                return;

            transform.position += Velocity * Time.deltaTime;
        }
    }
}