using Cysharp.Threading.Tasks;
using Scripts.Enemy;
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
    public class EnemiesFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly SimpleAssetLoader _simpleAssetLoader;

        public EnemiesFactory(
            IObjectResolver objectResolver,
            SimpleAssetLoader simpleAssetLoader)
        {
            _objectResolver = objectResolver;
            _simpleAssetLoader = simpleAssetLoader;
        }

        public async UniTask<EnemyComponent[]> Create()
        {
            var enemiesData = await _simpleAssetLoader.LoadAssetAsync<EnemiesData>(AssetKeys.ENEMIES_DATA);
            var parent = new GameObject("---Enemies---");
            var enemyComponents = new EnemyComponent[enemiesData.Enemies.Length];

            for (var index = 0; index < enemiesData.Enemies.Length; index++)
            {
                var enemyData = enemiesData.Enemies[index];
                var enemyGameObject = _objectResolver.Instantiate(enemyData.Prefab, parent.transform);
                var enemyComponent = enemyGameObject.GetComponent<EnemyComponent>();
                var spineAnimatorComponent = enemyGameObject.GetComponent<SpineAnimatorComponent>();

                var healthView = enemyGameObject.GetComponentInChildren<HealthViewElements>();
                var stateStatusView = enemyGameObject.GetComponentInChildren<CharacterBattleStatusViewElements>();

                var healthPresenter = _objectResolver.Resolve<HealthPresenter>();
                var stateStatusPresenter = _objectResolver.Resolve<CharacterBattleStatusPresenter>();

                var enemy = _objectResolver.Resolve<Enemy.Enemy>();

                stateStatusPresenter.Init(enemy, stateStatusView);
                enemy.Init(enemyData.EnemyStats, enemyData.StartWeapon.Weapon, spineAnimatorComponent);
                healthPresenter.Init(enemy, healthView);
                enemyComponent.Init(enemy);

                enemyComponent.gameObject.SetActive(false);
                enemyComponents[index] = enemyComponent;
            }

            return enemyComponents;
        }
    }
}