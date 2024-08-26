using System;

namespace Scripts.Interfaces
{
    public interface IAttackable
    {
        event Action<float> OnAttack;
    }
}