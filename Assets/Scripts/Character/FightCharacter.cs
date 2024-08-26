using System;
using Scripts.Enum;
using Scripts.Interfaces;
using Scripts.SpineAnimation;
using Scripts.Weapon;
using UnityEngine;
using UnityHFSM;

namespace Scripts.Character
{
    public abstract class FightCharacter : IAttackable,
        IHealth, IDamageable, IFsmEvents<CharacterStates>, IFight
    {
        public event Action<float> OnAttack;
        public event Action OnDead;
        public event Action<CharacterStates> OnStateChanged;
        public event Action<float> OnTimerUpdated;
        public event Action OnHealthChanged;
        public event Action OnStopFighting;
        public FightingCharacterStats CharacterStats => _fightingCharacterStats;
        public float CurrentHealth => _fightingCharacterStats.CurrentHealth;
        public float MaximumHealth => _fightingCharacterStats.MaximumHealth;
        public SpineAnimatorComponent SpineAnimator => _spineAnimator;

        [SerializeReference]
        protected WeaponItem _currentWeapon;
        [SerializeReference]
        protected WeaponItem _nextWeapon;
        [SerializeReference]
        protected FightingCharacterStats _fightingCharacterStats = new FightingCharacterStats();

        protected StateMachine<CharacterStates> _fsm = new StateMachine<CharacterStates>();
        protected SpineAnimatorComponent _spineAnimator;
        protected bool _isFighting;

        protected void Init(
            FightingCharacterStats stats,
            WeaponItem weaponItem,
            SpineAnimatorComponent spineAnimatorComponent)
        {
            _spineAnimator = spineAnimatorComponent;
            _fightingCharacterStats = stats;
            _currentWeapon = weaponItem;

            InitStates();
            InitTransition();

            _fsm.SetStartState(CharacterStates.Idle);
            _fsm.Init();
        }

        public virtual void Heal(float value = 0)
        {
            _fightingCharacterStats.CurrentHealth += value;

            if (_fightingCharacterStats.CurrentHealth > _fightingCharacterStats.MaximumHealth)
            {
                MaximumHeal();
            }

            OnHealthChanged?.Invoke();
        }

        public virtual void MaximumHeal()
        {
            _fightingCharacterStats.CurrentHealth = _fightingCharacterStats.MaximumHealth;
            OnHealthChanged?.Invoke();
        }

        public virtual void TakeDamage(float value)
        {
            if (!_isFighting)
            {
                return;
            }

            _fightingCharacterStats.CurrentHealth -= value * (100 / (100 + _fightingCharacterStats.Armor));

            OnHealthChanged?.Invoke();

            if (_spineAnimator.CurrentAsset ==
                _spineAnimator.GetReferenceAsset(_currentWeapon.Stats.WeaponType, CharacterStates.Attack))
            {
                return;
            }

            _spineAnimator.SetAnimationShot(_currentWeapon.Stats.WeaponType, CharacterStates.TakeDamage, false);
        }

        public void LogicFsm()
        {
            _fsm.OnLogic();
        }

        protected virtual void InitStates()
        {
            _fsm.AddState(CharacterStates.Idle,
                          onEnter: state =>
                                   {
                                       OnStateChanged?.Invoke(_fsm.ActiveStateName);
                                       OnTimerUpdated?.Invoke(0);
                                       _nextWeapon = null;
                                       _spineAnimator.SetAnimation(_currentWeapon.Stats.WeaponType,
                                                                   CharacterStates.Idle,
                                                                   true);
                                   });

            _fsm.AddState(CharacterStates.ChangeWeapon,
                          onEnter: state =>
                                   {
                                       OnStateChanged?.Invoke(_fsm.ActiveStateName);
                                       _spineAnimator.SetAnimation(_currentWeapon.Stats.WeaponType,
                                                                   CharacterStates.Idle,
                                                                   true);
                                   },
                          onLogic: state =>
                                   {
                                       OnTimerUpdated?.Invoke((float) Math.Round(_fightingCharacterStats
                                                                      .WeaponSwitchTime -
                                                                   state.timer.Elapsed,
                                                               1));
                                       if (state.timer.Elapsed > _fightingCharacterStats.WeaponSwitchTime)
                                       {
                                           _spineAnimator.SetAnimation(_nextWeapon.Stats.WeaponType,
                                                                       CharacterStates.Idle,
                                                                       true);
                                       }
                                   },
                          onExit: state =>
                                  {
                                      _currentWeapon = _nextWeapon;
                                      _nextWeapon = null;
                                      OnTimerUpdated?.Invoke(0);
                                  });

            _fsm.AddState(CharacterStates.PreparationAttack,
                          onEnter: state =>
                                   {
                                       OnStateChanged?.Invoke(_fsm.ActiveStateName);
                                       _spineAnimator.SetAnimation(_currentWeapon.Stats.WeaponType,
                                                                   CharacterStates.Idle,
                                                                   true);
                                   },
                          onLogic: state =>
                                   {
                                       OnTimerUpdated?.Invoke((float) Math.Round(_fightingCharacterStats
                                                                      .AttackPreparationTime -
                                                                   state.timer.Elapsed,
                                                               1));
                                   },
                          onExit: state =>
                                  {
                                      if (state.timer.Elapsed < _fightingCharacterStats.AttackPreparationTime)
                                      {
                                          state.timer.Pause();
                                      }

                                      if (!_isFighting ||
                                          _fightingCharacterStats.CurrentHealth <= 0)
                                      {
                                          state.timer.Resume();
                                      }
                                  },
                          canExit: state => state.timer.Elapsed > _fightingCharacterStats.AttackPreparationTime,
                          needsExitTime: true);

            _fsm.AddState(CharacterStates.Attack,
                          onEnter: state =>
                                   {
                                       OnStateChanged?.Invoke(_fsm.ActiveStateName);
                                       _spineAnimator.SetAnimation(_currentWeapon.Stats.WeaponType,
                                                                   CharacterStates.Idle,
                                                                   true);
                                   },
                          onLogic: state =>
                                   {
                                       if (state.timer.Elapsed > _currentWeapon.Stats
                                              .AttackSpeedTime)
                                       {
                                           _spineAnimator.SetAnimation(_currentWeapon.Stats.WeaponType,
                                                                       CharacterStates.Attack,
                                                                       false,
                                                                       entry =>
                                                                       {
                                                                           OnAttack?.Invoke(_fightingCharacterStats
                                                                              .DamageValue);
                                                                           _fsm.StateCanExit();
                                                                       });
                                       }
                                       else
                                       {
                                           OnTimerUpdated?.Invoke((float) Math.Round(_currentWeapon.Stats
                                                                      .AttackSpeedTime - state.timer.Elapsed,
                                                                   1));
                                       }
                                   },
                          onExit: state =>
                                  {
                                      if (state.timer.Elapsed < _currentWeapon.Stats
                                                                              .AttackSpeedTime)
                                      {
                                          state.timer.Pause();
                                      }

                                      if (!_isFighting ||
                                          _fightingCharacterStats.CurrentHealth <= 0)
                                      {
                                          state.timer.Resume();
                                      }
                                  },
                          needsExitTime: true);

            _fsm.AddState(CharacterStates.Dead,
                          onEnter: state =>
                                   {
                                       _fightingCharacterStats.CurrentHealth = 0;
                                       StopFighting();
                                       OnStopFighting?.Invoke();
                                       OnHealthChanged?.Invoke();
                                       OnStateChanged?.Invoke(_fsm.ActiveStateName);
                                       _spineAnimator.SetAnimation(_currentWeapon.Stats.WeaponType,
                                                                   CharacterStates.Dead,
                                                                   false,
                                                                   entry => { OnDead?.Invoke(); });
                                   });
        }

        protected virtual void InitTransition()
        {
            _fsm.AddTransition(CharacterStates.Idle, CharacterStates.PreparationAttack, transition => _isFighting);

            _fsm.AddTransition(new TransitionAfter<CharacterStates>(CharacterStates.ChangeWeapon,
                                                                    CharacterStates.PreparationAttack,
                                                                    _fightingCharacterStats.WeaponSwitchTime));

            _fsm.AddTransition(CharacterStates.PreparationAttack, CharacterStates.Attack);
            _fsm.AddTransition(CharacterStates.Attack,
                               CharacterStates.PreparationAttack,
                               transition => _nextWeapon == null);

            _fsm.AddTransitionFromAny(CharacterStates.Dead,
                                      transition => _fightingCharacterStats.CurrentHealth <= 0,
                                      forceInstantly: true);

            _fsm.AddTransitionFromAny(CharacterStates.Idle,
                                      transition =>
                                      {
                                          if (_fightingCharacterStats.CurrentHealth > 0)
                                          {
                                              return !_isFighting;
                                          }

                                          return false;
                                      },
                                      forceInstantly: true);

            _fsm.AddTransition(CharacterStates.Dead,
                               CharacterStates.Idle,
                               transition => _fightingCharacterStats.CurrentHealth > 0);
        }

        public void StartFight()
        {
            _isFighting = true;
        }

        public void StopFighting()
        {
            _isFighting = false;
        }
    }
}