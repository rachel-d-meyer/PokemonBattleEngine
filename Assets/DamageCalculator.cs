using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalculator
{



    internal static List<double[]> calc(Move m, ActivePokemon a, ActivePokemon d, double[] aStats, double[] dStats)
    {
        List<double[]> resultingStats = new List<double[]>();
        int fcHP = (int)dStats[0];
        int cHP = (int)aStats[0];
        TypeCheck _type = new TypeCheck();
        double mod = 1;
        if (aStats[0] > 0)
        {
            if (_type.isSuperEffective(m.Type, d.P))
            {
                mod = mod * 2;
            }
            if (_type.isNotEffective(m.Type, d.P))
            {
                mod = mod * 0.5;
            }
            if (_type.isImmune(m.Type, d.P))
            {
                mod = mod * 0;
            }
            if (a.P.Type.Equals(m.Type))
            {
                mod = mod * 1.5;
            }

            if (!m.Info.Equals("Heal") && !m.Type.Equals("Null"))
            {
                double typeA = 0;
                double typeD = 0;
                if (m.Attack.Equals("S"))
                {

                    typeA = a.P.Stats[0].SpAtk * aStats[3];
                    typeD = d.P.Stats[0].SpDef * dStats[3];

                }
                else if (m.Attack.Equals("P"))
                {

                    typeA = a.P.Stats[0].Atk * aStats[2];
                    typeD = d.P.Stats[0].Def * dStats[2];


                }
                double dam = ((((((2 * 50) / 5) + 2) * m.Power * typeA / typeD) / 50) + 2) * mod;
                int damage = (int)dam;
                if (m.Name.Equals("Sonic Boom"))
                {
                    damage = 20;
                }

                if (m.Info.Equals("Drain"))
                {
                    aStats[0] = aStats[0] + (damage / 2);

                    if (aStats[0] > a.P.Stats[0].HP)
                    {
                        aStats[0] = a.P.Stats[0].HP;
                    }



                    
                    else if (m.Info.Equals("AUp"))
                    {
                        aStats[1] = (int)modIncrease(aStats[1], 1);
                    }
                    else if (m.Info.Equals("DDownSpDDown"))
                    {
                        aStats[2] = modDecrease(aStats[2], 1);
                        aStats[4] = modDecrease(aStats[4], 1);
                    }

                    if (dStats[0] < 0)
                    {
                        dStats[0] = 0;
                    }


                }

                dStats[0] = dStats[0] - damage;


            }

            else if (m.Info.Equals("Heal"))
            {

                aStats[0] = aStats[0] + (a.P.Stats[0].HP / 2);
                if (aStats[0] > a.P.Stats[0].HP)
                {
                    aStats[0] = a.P.Stats[0].HP;
                }



            }
            else
            {


                if (m.Info.Equals("AUpDUp"))
                {

                    aStats[1] = modIncrease(aStats[1], 1);
                    aStats[2] = modIncrease(aStats[2], 1);

                }
                else if (m.Info.Equals("SpAUpSpDUp"))
                {

                    aStats[3] = modIncrease(aStats[3], 1);
                    aStats[4] = modIncrease(aStats[4], 1);


                }
                else if (m.Info.Equals("SpAUp2"))
                {


                    aStats[3] = modIncrease(aStats[3], 2);


                }
                else
                {

                }


            }
            if (aStats[0] < 0)
            {
                aStats[0] = 0;
            }
            if (dStats[0] < 0)
            {
                dStats[0] = 0;
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
