namespace Scripts.Interfaces
{
    public interface IFsmEvents<T> : IStateChange<T>, ITimerUpdate
    {
    }
}