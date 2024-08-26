using Scripts.Enemy;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemy Data", menuName = "Configs/Data/Enemy")]
    public class EnemyData : FightingCharacterData
    {
        public EnemyStats EnemyStats;

        private void OnValidate()
        {
            HealthValidate(EnemyStats);
        }
    }
}