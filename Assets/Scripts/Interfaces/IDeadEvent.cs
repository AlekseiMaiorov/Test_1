using System;

namespace Scripts.Interfaces
{
    public interface IDeadEvent
    {
        public event Action OnDead;
    }
}