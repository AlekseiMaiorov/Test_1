using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;
using Scripts.Enemy;

namespace Scripts.StateMachines.Battle.States
{
    public class BattleState : State
    {
        private readonly EnemiesProvider _provider;
        private readonly Player.Player _player;
        private EnemyComponent _enemyComponent;

        public BattleState(EnemiesProvider provider, Player.Player player)
        {
            _player = player;
            _provider = provider;
        }

        public override UniTask Enter()
        {
            _enemyComponent = _provider.GetCurrentEnemy();

            _player.OnAttack += _enemyComponent.Enemy.TakeDamage;
            _enemyComponent.Enemy.OnAttack += _player.TakeDamage;

            _player.OnStopFighting += _enemyComponent.Enemy.StopFighting;
            _enemyComponent.Enemy.OnStopFighting += _player.StopFighting;
            
            _enemyComponent.Enemy.OnDead += HandleEnemyDeath;
            _player.OnDead += HandlePlayerDeath;
            
            _player.StartFight();
            _enemyComponent.Enemy.StartFight();

            return UniTask.CompletedTask;
        }

        public override async UniTask Exit()
        {
            _player.StopFighting();
            _enemyComponent.Enemy.StopFighting();

            _enemyComponent.Enemy.MaximumHeal();
            await UniTask.NextFrame();
            _enemyComponent.gameObject.SetActive(false);

            _player.OnStopFighting -= _enemyComponent.Enemy.StopFighting;
            _enemyComponent.Enemy.OnStopFighting -= _player.StopFighting;

            _player.OnAttack -= _enemyComponent.Enemy.TakeDamage;
            _enemyComponent.Enemy.OnAttack -= _player.TakeDamage;
            
            _enemyComponent.Enemy.OnDead -= HandleEnemyDeath;
            _player.OnDead -= HandlePlayerDeath;
        }

        private void HandlePlayerDeath()
        {
            _stateMachine.Enter<LoseBattleState>().Forget();
        }

        private void HandleEnemyDeath()
        {
            _stateMachine.Enter<WinBattleState>().Forget();
        }
    }
}