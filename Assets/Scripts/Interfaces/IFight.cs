using System;

namespace Scripts.Interfaces
{
    public interface IFight
    {
        event Action OnStopFighting;
        public void StartFight();
        public void StopFighting();
    }
}