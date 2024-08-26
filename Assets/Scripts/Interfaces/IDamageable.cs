namespace Scripts.Interfaces
{
    public interface IDamageable : IDeadEvent
    {
        void TakeDamage(float damage);
    }
}