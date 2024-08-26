using System.Collections.Generic;
using System.Linq;

namespace Scripts.Enemy
{
    public class EnemiesProvider
    {
        public List<EnemyComponent> EnemiesPool => _enemies;
        private List<EnemyComponent> _enemies;

        public void Init(IEnumerable<EnemyComponent> enemyComponents)
        {
            _enemies = enemyComponents.ToList();
        }

        public EnemyComponent GetCurrentEnemy()
        {
            return _enemies.FirstOrDefault(component => component.gameObject.activeSelf);
        }
    }
}