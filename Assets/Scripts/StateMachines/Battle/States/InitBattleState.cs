using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;
using Scripts.Enemy;
using Scripts.Factories;

namespace Scripts.StateMachines.Battle.States
{
    public class InitBattleState : State
    {
        private readonly BattleUIFactory _battleUIFactory;
        private readonly PlayerFactory _playerFactory;
        private readonly EnemiesFactory _enemiesFactory;
        private readonly EnemiesProvider _enemiesProvider;

        public InitBattleState(
            PlayerFactory playerFactory,
            BattleUIFactory battleUIFactory,
            EnemiesFactory enemiesFactory,
            EnemiesProvider enemiesProvider)
        {
            _enemiesProvider = enemiesProvider;
            _enemiesFactory = enemiesFactory;
            _battleUIFactory = battleUIFactory;
            _playerFactory = playerFactory;
        }

        public override async UniTask Enter()
        {
            await _playerFactory.Create();
            await _battleUIFactory.Create();

            EnemyComponent[] enemyComponents = await _enemiesFactory.Create();
            _enemiesProvider.Init(enemyComponents);
            _stateMachine.Enter<PreBattleState>().Forget();
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}