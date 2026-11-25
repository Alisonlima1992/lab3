using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Models
{
    public class CEnemy
    {
        private double health;
        private double maxHealth;
        private double goldReward;

        public CEnemy(double maxHealth, double goldReward)
        {
            this.maxHealth = maxHealth;
            this.health = maxHealth;
            this.goldReward = goldReward;
        }

        public bool TakeDamage(double damage)
        {
            health -= damage;
            return health <= 0;
        }

        public void Reset(double newMaxHealth, double newGoldReward)
        {
            maxHealth = newMaxHealth;
            health = maxHealth;
            goldReward = newGoldReward;
        }

        public double Health => health;
        public double MaxHealth => maxHealth;
        public double GoldReward => goldReward;
        public double HealthPercent => health / maxHealth;
    }
}
