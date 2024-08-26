using System;
using System.Linq;
using Scripts.Enum;
using Scripts.Interfaces;
using Scripts.ScriptableObjects;
using Scripts.UI.View;
using Scripts.Weapon;

namespace Scripts.UI.Presenter
{
    public class BackpackPresenter: IDisposable
    {
        private BackpackViewElements _elements;
        private IUseItem _useItem;
        private WeaponsData _weaponsData;

        public void Init(BackpackViewElements elements, IUseItem useItem, WeaponsData weaponsData)
        {
            _weaponsData = weaponsData;
            _useItem = useItem;
            _elements = elements;
            
            _elements.ItemOne.onClick.AddListener(() => TakeWeaponItem(WeaponType.ColdWeapon));

            _elements.ItemTwo.onClick.AddListener(() => TakeWeaponItem(WeaponType.Pistol));
        }

        private void TakeWeaponItem(WeaponType type)
        {
            WeaponItem item = _weaponsData.WeaponDatas.Where(data => data.Weapon.Stats.WeaponType == type)
                                                    .Select(data => data.Weapon)
                                                    .FirstOrDefault();

            _useItem.UseItem(item);
        }
        
        public void Dispose()
        {
            _elements.ItemOne.onClick.RemoveAllListeners();
            _elements.ItemTwo.onClick.RemoveAllListeners();
        }
    }
}