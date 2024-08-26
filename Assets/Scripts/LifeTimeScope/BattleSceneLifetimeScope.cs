using Scripts.Enemy;
using Scripts.EntryPoint;
using Scripts.Factories;
using Scripts.StateMachines.Battle;
using Scripts.StateMachines.Battle.States;
using Scripts.UI.Presenter;
using VContainer;
using VContainer.Unity;

namespace Scripts.LifeTimeScope
{
    public class BattleSceneLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<BattleEntryPoint>();
            
            builder.Register<Player.Player>(Lifetime.Singleton).AsImplementedInterfaces().AsSelf();
            builder.Register<Enemy.Enemy>(Lifetime.Transient).AsImplementedInterfaces().AsSelf();

            builder.Register<PlayerFactory>(Lifetime.Singleton);
            builder.Register<EnemiesProvider>(Lifetime.Singleton);
            builder.Register<EnemiesFactory>(Lifetime.Singleton);
            builder.Register<EnemySpawner>(Lifetime.Singleton);
            builder.Register<BattleUIFactory>(Lifetime.Singleton);

            RegisterUIPresenters(builder);
            RegisterBattleStateMachine(builder);
        }

        private void RegisterBattleStateMachine(IContainerBuilder builder)
        {
            builder.Register<BattleStateMachine>(Lifetime.Singleton);
            builder.Register<InitBattleState>(Lifetime.Singleton);
            builder.Register<PreBattleState>(Lifetime.Singleton);
            builder.Register<SpawnEnemyState>(Lifetime.Singleton);
            builder.Register<BattleState>(Lifetime.Singleton);
            builder.Register<WinBattleState>(Lifetime.Singleton);
            builder.Register<LoseBattleState>(Lifetime.Singleton);
            builder.Register<EndBattleState>(Lifetime.Singleton);
        }

        private void RegisterUIPresenters(IContainerBuilder builder)
        {
            builder.Register<BackpackPresenter>(Lifetime.Singleton);
            builder.Register<BattleButtonsPresenter>(Lifetime.Singleton);
            builder.Register<DamageInfoPresenter>(Lifetime.Singleton);

            builder.Register<HealthPresenter>(Lifetime.Transient);
            builder.Register<CharacterBattleStatusPresenter>(Lifetime.Transient);
        }
    }
}