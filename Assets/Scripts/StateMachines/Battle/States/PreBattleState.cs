using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;

namespace Scripts.StateMachines.Battle.States
{
    public class PreBattleState : State
    {
        public override UniTask Enter()
        {
            return UniTask.CompletedTask;
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}