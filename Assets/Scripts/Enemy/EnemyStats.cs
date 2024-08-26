using System;
using Scripts.Character;

namespace Scripts.Enemy
{
    [Serializable]
    public class EnemyStats : FightingCharacterStats
    {
        public string Name;
        public float SpawnChance;
    }
}