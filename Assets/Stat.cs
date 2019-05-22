using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets
{
    public class Stat
    {
        public double hp;
        public double attack;
        public double defence;
        public double specialAttack;
        public double specialDefence;

        public Stat(double hp, double attack, double defence, double specialAttack, double specialDefence)
        {
            this.hp = hp;
            this.attack = attack;
            this.defence = defence;
            this.specialAttack = specialAttack;
            this.specialDefence = specialDefence;
        }

        public Stat Copy()
        {
            return new Stat(hp, attack, defence, specialAttack, specialDefence);
        }

        public void CopyFrom(Stat other)
        {
            hp = other.hp;
            attack = other.attack;
            defence = other.defence;
            specialAttack = other.specialAttack;
            specialDefence = other.specialDefence;
        }
    }
}
