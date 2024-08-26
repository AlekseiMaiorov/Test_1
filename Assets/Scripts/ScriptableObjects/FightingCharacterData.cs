using Scripts.Character;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    public abstract class FightingCharacterData : ScriptableObject
    {
        public bool IsStartHealthEqualsMax;
        public GameObject Prefab;
        public WeaponData StartWeapon;

        protected virtual void HealthValidate(FightingCharacterStats stats)
        {
            if (IsStartHealthEqualsMax)
            {
                stats.CurrentHealth = stats.MaximumHealth;
            }

            if (stats.CurrentHealth > stats.MaximumHealth)
            {
                stats.CurrentHealth = stats.MaximumHealth;
            }
        }
    }
}