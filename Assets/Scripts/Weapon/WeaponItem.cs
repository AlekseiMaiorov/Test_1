using System;
using UnityEngine;

namespace Scripts.Weapon
{
    [Serializable]
    public class WeaponItem : Item
    {
        public WeaponStats Stats => _weaponStats;
        
        [SerializeField]
        private WeaponStats _weaponStats;
    }

    public abstract class Item
    {
        public ItemType Type => _itemType;

        [SerializeField]
        protected ItemType _itemType;
    }

    public enum ItemType
    {
        Weapon
    }
}