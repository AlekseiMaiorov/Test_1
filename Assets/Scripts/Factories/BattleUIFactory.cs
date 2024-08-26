using Cysharp.Threading.Tasks;
using Scripts.ScriptableObjects;
using Scripts.Services.AssetManagement;
using Scripts.StateMachines.Battle;
using Scripts.UI.Presenter;
using Scripts.UI.View;
using UnityEngine;
using VContainer;

namespace Scripts.Factories
{
    public class BattleUIFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly SimpleAssetLoader _simpleAssetLoader;
        private readonly BackpackPresenter _backpackPresenter;
        private readonly BattleButtonsPresenter _battleButtonsPresenter;
        private readonly DamageInfoPresenter _damageInfoPresenter;
        private readonly Player.Player _player;

        public BattleUIFactory(
            IObjectResolver objectResolver,
            SimpleAssetLoader simpleAssetLoader,
            BackpackPresenter backpackPresenter,
            BattleButtonsPresenter battleButtonsPresenter,
            DamageInfoPresenter damageInfoPresenter,
            Player.Player player)
        {
            _player = player;
            _damageInfoPresenter = damageInfoPresenter;
            _battleButtonsPresenter = battleButtonsPresenter;
            _backpackPresenter = backpackPresenter;
            _objectResolver = objectResolver;
            _simpleAssetLoader = simpleAssetLoader;
        }

        public async UniTask<GameObject> Create()
        {
            var canvasPrefab = await _simpleAssetLoader.LoadAssetAsync<GameObject>(AssetKeys.CANVAS_BATTLE);
            var weaponsData = await _simpleAssetLoader.LoadAssetAsync<WeaponsData>(AssetKeys.WEAPONS_DATA);
            var canvas = Object.Instantiate(canvasPrefab, null, true);

            var backpackViewElements = canvas.GetComponentInChildren<BackpackViewElements>();
            var battleButtonsViewElements = canvas.GetComponentInChildren<BattleButtonsViewElements>();
            var damageInfoViewElements = canvas.GetComponentInChildren<DamageInfoViewElements>();

            var battleStateMachine = _objectResolver.Resolve<BattleStateMachine>();

            _damageInfoPresenter.Init(damageInfoViewElements, _player.CharacterStats.DamageValue);
            _battleButtonsPresenter.Init(battleButtonsViewElements, _player, battleStateMachine);
            _backpackPresenter.Init(backpackViewElements, _player, weaponsData);

            return canvas;
        }
    }
}