using UnityEngine;
using Zenject;

namespace Aspects.Services.Factories.Scripts
{
    public class ComponentFactory<T> : PlaceholderFactory<T> where T : Component
    {
        public T Create(Transform transform)
        {
            T instance = Create();
            instance.transform.parent = transform;
            return instance;
        }
    }
}