
using UnityEngine;

namespace PrjectSurvivor
{
    public class DamageSystem
    {
        public static void CalculateDamage(float baseDamage, IEnemy enemy, int maxNoramalDamage = 2,
            float criticalDamageTime = 5)
        {
            baseDamage *= Global.DamageRate.Value;
            if (Random.Range(0, 1f) < Global.CriticalRate.Value)
            {
                enemy.Hurt(baseDamage * Random.Range(2f, criticalDamageTime), false, true);
            }
            else
            {
                enemy.Hurt(baseDamage + Random.Range(-1, maxNoramalDamage));
            }

        }
    }
}
