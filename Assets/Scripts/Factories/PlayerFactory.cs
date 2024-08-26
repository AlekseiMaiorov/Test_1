using Cysharp.Threading.Tasks;
using Scripts.Player;
using Scripts.ScriptableObjects;
using Scripts.Services.AssetManagement;
using Scripts.SpineAnimation;
using Scripts.UI.Presenter;
using Scripts.UI.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Scripts.Factories
{
    public class PlayerFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly SimpleAssetLoader _simpleAssetLoader;
        private readonly Player.Player _player;

        public PlayerFactory(
            IObjectResolver objectResolver,
            SimpleAssetLoader simpleAssetLoader,
            Player.Player player)
        {
            _player = player;
            _objectResolver = objectResolver;
            _simpleAssetLoader = simpleAssetLoader;
        }

        public async UniTask<PlayerComponent> Create()
        {
            var playerData = await _simpleAssetLoader.LoadAssetAsync<PlayerData>(AssetKeys.PLAYER_DATA);
            var parent = new GameObject("---Player---");
            
            var playerGameObject = _objectResolver.Instantiate(playerData.Prefab, parent.transform);
            
            var playerComponent = playerGameObject.GetComponent<PlayerComponent>();
            var spineAnimatorComponent = playerGameObject.GetComponent<SpineAnimatorComponent>();

            var healthView = playerGameObject.GetComponentInChildren<HealthViewElements>();
            var stateStatusView = playerGameObject.GetComponentInChildren<CharacterBattleStatusViewElements>();

            var healthPresenter = _objectResolver.Resolve<HealthPresenter>();
            var stateStatusPresenter = _objectResolver.Resolve<CharacterBattleStatusPresenter>();

            stateStatusPresenter.Init(_player, stateStatusView);
            _player.Init(playerData.PlayerStats, playerData.StartWeapon.Weapon, spineAnimatorComponent);
            healthPresenter.Init(_player, healthView);
            playerComponent.Init(_player);

            return playerComponent;
        }
    }
}