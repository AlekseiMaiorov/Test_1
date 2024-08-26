using Scripts.StateMachines.Game.States;

namespace Scripts.StateMachines.Game
{
    public class GameStateMachine : Common.StateMachine.StateMachine
    {
        public GameStateMachine(BootstrapState bootstrapState) :
            base(states: bootstrapState)
        {
        }
    }
}