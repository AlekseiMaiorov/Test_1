using Scripts.Character;
using UnityEngine;

namespace Scripts.Enemy
{
    public class EnemyComponent : MonoBehaviour
    {
        public Enemy Enemy => _enemy;

        [SerializeReference]
        private Enemy _enemy;

        public void Init(Enemy enemy)
        {
            _enemy = enemy;
        }

        public void ResetPoseAnimation()
        {
            _enemy.SpineAnimator.ResetPose();
        }

        private void Update()
        {
            _enemy.LogicFsm();
        }
    }
}