using System;
using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;

namespace Scripts.StateMachines.Battle.States
{
    public class LoseBattleState : State
    {
        public override async UniTask Enter()
        {
            _stateMachine.Enter<EndBattleState>().Forget();
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}