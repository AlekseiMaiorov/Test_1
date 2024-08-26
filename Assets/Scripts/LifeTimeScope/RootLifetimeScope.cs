using Scripts.EntryPoint;
using Scripts.Services.AssetManagement;
using Scripts.Services.SceneLoader;
using Scripts.StateMachines.Game;
using Scripts.StateMachines.Game.States;
using VContainer;
using VContainer.Unity;

namespace Scripts.LifeTimeScope
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<GameEntryPoint>();
            RegisterGameStateMachine(builder);
            RegisterServices(builder);
        }

        private void RegisterGameStateMachine(IContainerBuilder builder)
        {
            builder.Register<GameStateMachine>(Lifetime.Singleton);
            builder.Register<BootstrapState>(Lifetime.Singleton);
        }

        private void RegisterServices(IContainerBuilder builder)
        {
            builder.Register<SimpleAssetLoader>(Lifetime.Singleton);
            builder.Register<AddressablesSceneLoader>(Lifetime.Singleton);
        }
    }
}