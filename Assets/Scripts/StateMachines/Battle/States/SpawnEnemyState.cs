using System;
using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;
using Scripts.Enemy;

namespace Scripts.StateMachines.Battle.States
{
    public class SpawnEnemyState: State
    {
        private readonly EnemySpawner _spawner;

        public SpawnEnemyState(EnemySpawner spawner)
        {
            _spawner = spawner;
        }
        public override async UniTask Enter()
        {
            EnemyComponent enemyComponent = _spawner.Spawn();
            
            enemyComponent.ResetPoseAnimation();
            
            _stateMachine.Enter<BattleState>().Forget();
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}