using Cysharp.Threading.Tasks;
using Scripts.StateMachines.Game;
using Scripts.StateMachines.Game.States;
using VContainer.Unity;

namespace Scripts.EntryPoint
{
    public class GameEntryPoint : IInitializable
    {
        private readonly GameStateMachine _gameStateMachine;

        public GameEntryPoint(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.Enter<BootstrapState>().Forget();
        }
    }
}