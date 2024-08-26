using System;
using Scripts.Enum;
using Scripts.Interfaces;
using Scripts.UI.View;

namespace Scripts.UI.Presenter
{
    public class CharacterBattleStatusPresenter: IDisposable
    {
        private IFsmEvents<CharacterStates> _playerFsm;
        private CharacterBattleStatusViewElements _viewElements;

        public void Init(IFsmEvents<CharacterStates> playerFsm, CharacterBattleStatusViewElements viewElements)
        {
            _viewElements = viewElements;
            _playerFsm = playerFsm;

            _playerFsm.OnStateChanged += VisibleStatusViewElements;
            _playerFsm.OnTimerUpdated += UpdateTimerText;
        }

        private void UpdateTimerText(float value)
        {
            _viewElements.Timer.text = value.ToString("F1");
        }

        private void VisibleStatusViewElements(CharacterStates state)
        {
            switch (state)
            {
                case CharacterStates.ChangeWeapon:
                case CharacterStates.PreparationAttack:
                case CharacterStates.Attack:
                case CharacterStates.TakeDamage:
                    _viewElements.Timer.gameObject.SetActive(true);
                    break;
                case CharacterStates.Idle:
                case CharacterStates.Dead:
                    _viewElements.Timer.gameObject.SetActive(false);
                    break;
                default:
                    _viewElements.Timer.gameObject.SetActive(false);
                    break;
            }

            _viewElements.State.text = state.ToString();
        }

        public void Dispose()
        {
            _playerFsm.OnStateChanged -= VisibleStatusViewElements;
            _playerFsm.OnTimerUpdated -= UpdateTimerText;
        }
    }
}