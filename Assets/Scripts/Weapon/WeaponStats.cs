using System;
using Scripts.Enum;
using UnityEngine.Serialization;

namespace Scripts.Weapon
{
    [Serializable]
    public class WeaponStats
    {
        public float AttackSpeedTime;
        public WeaponType WeaponType;
    }
}