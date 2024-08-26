using Scripts.Character;
using Scripts.SpineAnimation;
using Scripts.Weapon;

namespace Scripts.Enemy
{
    public class Enemy : FightCharacter
    {
        public void Init(EnemyStats stats, WeaponItem weaponItem, SpineAnimatorComponent spineAnimatorComponent)
        {
            base.Init(stats, weaponItem, spineAnimatorComponent);
        }
    }
}