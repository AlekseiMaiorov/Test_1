using Scripts.Character;
using Scripts.Enum;
using Scripts.Interfaces;
using Scripts.SpineAnimation;
using Scripts.Weapon;

namespace Scripts.Player
{
    public class Player : FightCharacter, IUseItem
    {
        public void Init(PlayerStats stats, WeaponItem weaponItem, SpineAnimatorComponent spineAnimatorComponent)
        {
            base.Init(stats, weaponItem, spineAnimatorComponent);
        }

        public void UseItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:

                    if (!_isFighting)
                    {
                        return;
                    }

                    if (_currentWeapon == item)
                    {
                        return;
                    }

                    if (_fightingCharacterStats.CurrentHealth <= 0)
                    {
                        return;
                    }

                    if (_nextWeapon == item)
                    {
                        return;
                    }

                    _nextWeapon = (WeaponItem) item;

                    if (_fsm.ActiveState.name == CharacterStates.PreparationAttack)
                    {
                        _fsm.RequestStateChange(CharacterStates.ChangeWeapon, true);
                    }
                    else
                    {
                        _fsm.RequestStateChange(CharacterStates.ChangeWeapon);
                    }
                    
                    break;
            }
        }
    }
}