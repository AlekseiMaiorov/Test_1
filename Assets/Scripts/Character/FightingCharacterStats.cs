using System;

namespace Scripts.Character
{
    [Serializable]
    public class FightingCharacterStats
    {
        public float Armor;
        public float AttackPreparationTime;
        public float DamageValue;
        public float CurrentHealth;
        public float MaximumHealth;
        public float WeaponSwitchTime;
    }
}