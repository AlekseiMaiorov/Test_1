using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;
using Scripts.Services.AssetManagement;
using Scripts.Services.SceneLoader;

namespace Scripts.StateMachines.Game.States
{
    public class BootstrapState : State
    {
        private AddressablesSceneLoader _addressablesSceneLoader;

        public BootstrapState(AddressablesSceneLoader addressablesSceneLoader)
        {
            _addressablesSceneLoader = addressablesSceneLoader;
        }

        public override async UniTask Enter()
        {
            await _addressablesSceneLoader.LoadSceneAsync(AssetKeys.BATTLE_SCENE);
        }

        public override UniTask Exit()
        {
            return UniTask.CompletedTask;
        }
    }
}