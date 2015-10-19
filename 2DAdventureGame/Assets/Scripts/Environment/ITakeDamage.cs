

using UnityEngine;
public interface ITakeDamage
{
    void TakeDamage(int damage, GameObject instigator);
    void TakePeriodicDamage(int duration, int damage,GameObject instigator);
}

