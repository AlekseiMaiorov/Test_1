using Scripts.Player;
using UnityEngine;

namespace Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Configs/Data/Player")]
    public class PlayerData : FightingCharacterData
    {
        public PlayerStats PlayerStats;

        private void OnValidate()
        {
            HealthValidate(PlayerStats);
        }
    }
}