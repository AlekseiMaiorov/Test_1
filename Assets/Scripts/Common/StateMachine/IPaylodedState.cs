using Cysharp.Threading.Tasks;

namespace Scripts.Common.StateMachine
{
    public interface IPaylodedState<TPayload> : IExitableState
    {
        UniTask Enter(TPayload payload);
    }
}