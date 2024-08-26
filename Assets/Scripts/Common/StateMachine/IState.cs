using Cysharp.Threading.Tasks;

namespace Scripts.Common.StateMachine
{
    public interface IState : IExitableState
    {
        UniTask Enter();
    }
}