using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemySpawner
    {
        private readonly EnemiesProvider _enemiesProvider;
        private readonly System.Random _random;

        public EnemySpawner(EnemiesProvider enemiesProvider)
        {
            _enemiesProvider = enemiesProvider;
            _random = new System.Random();
        }

        public EnemyComponent Spawn()
        {
            var totalChance = 0f;
            foreach (var enemy in _enemiesProvider.EnemiesPool)
            {
                var enemyStats = (EnemyStats) enemy.Enemy.CharacterStats;
                totalChance += enemyStats.SpawnChance;
            }

            var randomValue = (float) (_random.NextDouble() * totalChance);
            var cumulativeChance = 0f;

            foreach (var enemy in _enemiesProvider.EnemiesPool)
            {
                var enemyStats = (EnemyStats) enemy.Enemy.CharacterStats;
                cumulativeChance += enemyStats.SpawnChance;
                if (randomValue <= cumulativeChance)
                {
                    enemy.gameObject.SetActive(true);
                    return enemy;
                }
            }

            Debug.LogError("Ошибка спавна врага. Вернул первый элемент");
            return _enemiesProvider.EnemiesPool[0];
        }
    }
}