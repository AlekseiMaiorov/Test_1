using Cysharp.Threading.Tasks;

namespace Scripts.Common.StateMachine
{
    public interface IExitableState
    {
        void Initialize(StateMachine stateMachine); 
        UniTask Exit();
    }
}