using System;

namespace Gamelogic
{
    public interface IActivatable
    {
        public bool IsActive {get; set;}

        public event Action<bool> OnActivityChangedEvent;
    }
}