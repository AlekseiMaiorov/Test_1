using Scripts.Character;
using UnityEngine;

namespace Scripts.Player
{
    public class PlayerComponent : MonoBehaviour
    {
        [SerializeReference]
        private Player _player;

        public void Init(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            _player.LogicFsm();
        }
    }
}