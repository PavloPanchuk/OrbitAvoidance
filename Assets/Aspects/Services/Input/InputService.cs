using System;
using UnityEngine;

namespace Aspects.Services.InputService
{
    public class InputService : IInputService
    {
        public event Action SpaceClicked;

        public void Tick()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                SpaceClicked?.Invoke();
        }
    }
}