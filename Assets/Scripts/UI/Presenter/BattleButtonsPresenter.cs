using System;
using Cysharp.Threading.Tasks;
using Scripts.Common.StateMachine;
using Scripts.Interfaces;
using Scripts.StateMachines.Battle;
using Scripts.StateMachines.Battle.States;
using Scripts.UI.View;
using UnityEngine;

namespace Scripts.UI.Presenter
{
    public class BattleButtonsPresenter : IDisposable
    {
        private BattleButtonsViewElements _elements;
        private IHealth _health;
        private BattleStateMachine _battleStateMachine;

        public void Init(BattleButtonsViewElements elements, IHealth health, BattleStateMachine battleStateMachine)
        {
            _battleStateMachine = battleStateMachine;
            _health = health;
            _elements = elements;

            _battleStateMachine.OnStateChanged += ChangeElementsVisible;

            _elements.ResetHealth.onClick.AddListener(ResetHealthAndCheckState);

            _elements.Start.onClick.AddListener(StartBattle);

            _elements.End.onClick.AddListener(EndBattle);
        }

        private void StartBattle()
        {
            _battleStateMachine.Enter<SpawnEnemyState>().Forget();
        }

        private void EndBattle()
        {
            _battleStateMachine.Enter<EndBattleState>().Forget();
        }

        private void ResetHealthAndCheckState()
        {
            _health.MaximumHeal();

            if (_battleStateMachine.CurrentState is not PreBattleState)
            {
                _battleStateMachine.Enter<EndBattleState>().Forget();
            }
        }

        private void ChangeElementsVisible(IExitableState state)
        {
            switch (state)
            {
                case InitBattleState initBattleState:
                case PreBattleState preBattleState:
                    ChangElementsVisible(true);
                    break;
                case LoseBattleState loseBattleState:
                case EndBattleState endBattleState:
                    _elements.ResetHealth.gameObject.SetActive(true);
                    _elements.Start.gameObject.SetActive(false);
                    _elements.End.gameObject.SetActive(false);
                    break;
                case WinBattleState winBattleState:
                case SpawnEnemyState spawnEnemyState:
                case BattleState battleState:
                    ChangElementsVisible(false);
                    break;
                default:
                    Debug.LogError(nameof(_battleStateMachine.CurrentState) + " нет логики выполнения");
                    break;
            }
        }

        private void ChangElementsVisible(bool visible)
        {
            _elements.ResetHealth.gameObject.SetActive(visible);
            _elements.Start.gameObject.SetActive(visible);
            _elements.End.gameObject.SetActive(!visible);
        }

        public void Dispose()
        {
            _battleStateMachine.OnStateChanged -= ChangeElementsVisible;
            _elements.ResetHealth.onClick.RemoveAllListeners();
            _elements.Start.onClick.RemoveAllListeners();
            _elements.End.onClick.RemoveAllListeners();
        }
    }
}