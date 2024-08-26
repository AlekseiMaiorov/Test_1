using Scripts.UI.View;

namespace Scripts.UI.Presenter
{
    public class DamageInfoPresenter
    {
        private DamageInfoViewElements _damageInfoViewElements;

        public void Init(DamageInfoViewElements damageInfoViewElements, float damageValue)
        {
            _damageInfoViewElements = damageInfoViewElements;

            _damageInfoViewElements.DamageValue.text = damageValue.ToString();
        }
    }
}