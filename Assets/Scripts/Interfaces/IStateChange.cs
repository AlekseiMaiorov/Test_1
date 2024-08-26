using System;

namespace Scripts.Interfaces
{
    public interface IStateChange<T>
    {
        event Action<T> OnStateChanged;
    }
}