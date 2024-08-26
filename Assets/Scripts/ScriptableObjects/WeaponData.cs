using Scripts.Weapon;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapon Data", menuName = "Configs/Data/Weapon")]
    public class WeaponData : ScriptableObject
    {
        public WeaponItem Weapon;
    }
}