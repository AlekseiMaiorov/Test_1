using Scripts.Common.StateMachine;
using Scripts.StateMachines.Battle.States;

namespace Scripts.StateMachines.Battle
{
    public class BattleStateMachine : StateMachine
    {
        public BattleStateMachine(
            InitBattleState initBattleState,
            PreBattleState preBattleState,
            SpawnEnemyState spawnEnemyState,
            BattleState battleState,
            WinBattleState winBattleState,
            LoseBattleState loseBattleState,
            EndBattleState endBattleState) :
            base(states: new IExitableState[]
                 {
                     initBattleState,
                     preBattleState,
                     spawnEnemyState,
                     battleState,
                     winBattleState,
                     loseBattleState,
                     endBattleState
                 })
        {
        }
    }
}