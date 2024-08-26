using System;
using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;

namespace Scripts.StateMachines.Battle.States
{
    public class WinBattleState: State
    {
        public override async UniTask Enter()
        {
            _stateMachine.Enter<SpawnEnemyState>().Forget();
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}