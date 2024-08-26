using System;

namespace Scripts.Interfaces
{
    public interface IHealth
    {
        event Action OnHealthChanged;
        public float CurrentHealth { get; }
        public float MaximumHealth { get; }
        public void Heal(float value);
        public void MaximumHeal();
    }
}