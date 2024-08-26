using System;

namespace Scripts.Interfaces
{
    public interface ITimerUpdate
    {
        event Action<float> OnTimerUpdated;
    }
}