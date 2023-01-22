using System;
using Zenject;

namespace Aspects.Services.InputService
{
    public interface IInputService : ITickable
    {
        event Action SpaceClicked;
    }
}