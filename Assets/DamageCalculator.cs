using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class DamageCalculator
{



    internal static List<Stat> calc(Move m, ActivePokemon a, ActivePokemon d, Stat aStats, Stat dStats)
    {
        List<Stat> resultingStats = new List<Stat>();
        int fcHP = (int)dStats.hp;
        int cHP = (int)aStats.hp;
        TypeCheck type = new TypeCheck();
        double mod = 1;
        if (aStats.hp > 0)
        {
            if (type.isSuperEffective(m.Type, d.P))
            {
                mod = mod * 2;
            }
            if (type.isNotEffective(m.Type, d.P))
            {
                mod = mod * 0.5;
            }
            if (type.isImmune(m.Type, d.P))
            {
                mod = mod * 0;
            }
            if (a.P.Type.Equals(m.Type))
            {
                mod = mod * 1.5;
            }

            if (m.Info != Move.InfoType.HEAL && !m.Type.Equals("Null"))
            {
                double typeA = 0;
                double typeD = 0;
                if (m.Attack.Equals("S"))
                {

                    typeA = a.P.Stats[0].SpAtk * aStats.specialAttack;
                    typeD = d.P.Stats[0].SpDef * dStats.specialAttack;

                }
                else if (m.Attack.Equals("P"))
                {

                    typeA = a.P.Stats[0].Atk * aStats.defence;
                    typeD = d.P.Stats[0].Def * dStats.defence;

                    if(m.Info == Move.InfoType.FOE)
                    {
                        typeA = d.P.Stats[0].Atk * dStats.attack;
                    }

                }
                double dam = ((((((2 * 50) / 5) + 2) * m.Power * typeA / typeD) / 50) + 2) * mod;
                int damage = (int)dam;
              
                if (m.Name.Equals("Sonic Boom"))
                {
                    damage = 20;
                }
                if (m.Name.Equals("Seismic Toss"))
                {
                    damage = 50;
                }

                switch (m.Info)
                {
                    case Move.InfoType.DRAIN:
                        aStats.hp = aStats.hp + (damage / 2);
                        if (aStats.hp > a.P.Stats[0].HP)
                        {
                            aStats.hp = a.P.Stats[0].HP;
                        }
             
                        break;

                    case Move.InfoType.LEECH:
                        aStats.hp = aStats.hp + (int)(damage / 1.5);
                        if (aStats.hp > a.P.Stats[0].HP)
                        {
                            aStats.hp = a.P.Stats[0].HP;
                        }
                       
                        break;

                    case Move.InfoType.ATTACK_UP:
                        aStats.attack = modIncrease(aStats.attack, 1);
                        break;

                    case Move.InfoType.DEFENCE_DOWN_SPECIAL_DEFENCE_DOWN:
                        aStats.defence = modDecrease(aStats.defence, 1);
                        aStats.specialDefence = modDecrease(aStats.specialDefence, 1);
                        break;

                    case Move.InfoType.FOE_SPECIAL_ATTACK_DOWN:
                        dStats.specialAttack = modDecrease(dStats.specialAttack, 1);
                        break;
                }


                
                
          

                dStats.hp = dStats.hp - damage;


            }

            else if (m.Info == Move.InfoType.HEAL)
            {

                aStats.hp = aStats.hp + (a.P.Stats[0].HP / 2);
                if (aStats.hp > a.P.Stats[0].HP)
                {
                    aStats.hp = a.P.Stats[0].HP;
                }



            }
            else
            {
                switch (m.Info)
                {
                    case Move.InfoType.ATTACK_UP_DEFENCE_UP:
                        aStats.attack = modIncrease(aStats.attack, 1);
                        aStats.defence = modIncrease(aStats.defence, 1);

                        break;

                    case Move.InfoType.SPECIAL_ATTACK_UP_SPECIAL_DEFENCE_UP:
                        aStats.specialAttack = modIncrease(aStats.specialAttack, 1);
                        aStats.specialDefence = modIncrease(aStats.specialDefence, 1);

                        break;

                    case Move.InfoType.SPECIAL_ATTACK_UP_2:
                        aStats.specialAttack = modIncrease(aStats.specialAttack, 2);

                        break;

                    case Move.InfoType.ATTACK_UP_2:
                        aStats.attack = modIncrease(aStats.attack, 2);

                        break;

                    case Move.InfoType.DEFENCE_UP:
                        aStats.defence = modIncrease(aStats.defence, 1);

                        break;
                }

            }
            if (aStats.hp < 0)
            {
                aStats.hp = 0;
            }
            if (dStats.hp < 0)
            {
                dStats.hp = 0;
            }
        }
        resultingStats.Add(aStats);
        resultingStats.Add(dStats);

        return resultingStats;
    }


    static double modIncrease(double modd, int stages)
    {
        do
        {

            if (modd < 4 && modd >= 1)
            {
                modd = modd + 0.5;

            }
            else if (modd < 1)
            {
                modd = modUp(modd);


            }
            stages = stages - 1;
        } while (stages != 0);



        return modd;
    }

    static double modDecrease(double modd, int stages)
    {

        do
        {
            if (modd > 1)
            {
                modd = modd - 0.5;

            }
            else if (modd > 0.25 && modd <= 1)
            {
                modd = modDown(modd);
            }

            stages = stages - 1;
        } while (stages != 0);


        return modd;
    }

    static double modDown(double modd)
    {
        Debug.Log("Down");
        if (modd == 0.67)
        {
            modd = 0.5;
        }
        else if (modd == 1)
        {
            modd = 0.67;
        }
        else if (modd == 0.5)
        {
            modd = 0.4;
        }
        else if (modd == 0.4)
        {
            modd = 0.33;
        }
        else if (modd == 0.33)
        {
            modd = 0.3;
        }
        else if (modd == 0.3)
        {
            modd = 0.27;
        }
        return modd;
    }

    static double modUp(double modd)
    {
        if (modd == 0.67)
        {
            modd = 1;
        }
        else if (modd == 0.5)
        {
            modd = 0.67;
        }
        else if (modd == 0.4)
        {
            modd = 0.5;
        }
        else if (modd == 0.33)
        {
            modd = 0.4;
        }
        else if (modd == 0.3)
        {
            modd = 0.33;
        }
        else if (modd == 0.27)
        {
            modd = 0.3;
        }

        return modd;
    }
}
