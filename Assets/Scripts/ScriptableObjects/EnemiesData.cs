using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Enemies Data", menuName = "Configs/Data/Enemies Data")]
    public class EnemiesData : ScriptableObject
    {
        public EnemyData[] Enemies;
    }
}