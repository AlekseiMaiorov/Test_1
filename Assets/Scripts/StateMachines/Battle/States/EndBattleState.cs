using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;

namespace Scripts.StateMachines.Battle.States
{
    public class EndBattleState : State
    {
        private readonly Player.Player _player;

        public EndBattleState(Player.Player player)
        {
            _player = player;
        }

        public override UniTask Enter()
        {
            if (_player.CurrentHealth > 0)
            {
                _stateMachine.Enter<PreBattleState>().Forget();
            }

            return UniTask.CompletedTask;
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}