using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Weapons", menuName = "Configs/Data/Weapons")]
    public class WeaponsData : ScriptableObject
    {
        public WeaponData[] WeaponDatas;
    }
}