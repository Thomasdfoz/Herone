using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Game.Scripts
{
    public class Damage
    {
        type type;
        typeDamage typeDamage;
        private int damageVal;

        
        public Damage(int val, type type, typeDamage typeDamage)
        {
            this.DamageVal = val;
            this.Type = type;
            this.TypeDamage = typeDamage;

        }

        public int DamageVal { get => damageVal; set => damageVal = value; }
        internal type Type { get => type; set => type = value; }
        internal typeDamage TypeDamage { get => typeDamage; set => typeDamage = value; }
    }
}
