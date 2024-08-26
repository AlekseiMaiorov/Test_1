using Cysharp.Threading.Tasks;
using Scripts.StateMachines.Battle;
using Scripts.StateMachines.Battle.States;
using VContainer.Unity;

namespace Scripts.EntryPoint
{
    public class BattleEntryPoint : IInitializable
    {
        private readonly BattleStateMachine _battleStateMachine;

        public BattleEntryPoint(BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
        }

        public void Initialize()
        {
            _battleStateMachine.Enter<InitBattleState>().Forget();
        }
    }
}