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
        double mod = 1;
        if (aStats[0] > 0)
        {
            if (isSuperEffective(m, d.P))
            {
                mod = mod * 2;
            }
            if (isNotEffective(m, d.P))
            {
                mod = mod * 0.5;
            }
            if (isImmune(m, d.P))
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



    static bool isSuperEffective(Move m, Pokemon d)
    {

        String moveType = m.Type;
        String pType = d.Type;

        if (moveType.Equals("Water"))
        {
            if (pType.Equals("Fire") || pType.Equals("Ground") || pType.Equals("Rock"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Grass"))
        {
            if (pType.Equals("Water") || pType.Equals("Ground") || pType.Equals("Rock"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fire"))
        {
            if (pType.Equals("Ice") || pType.Equals("Steel") || pType.Equals("Bug") || pType.Equals("Grass"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Electric"))
        {
            if (pType.Equals("Water") || pType.Equals("Flying"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ice"))
        {
            if (pType.Equals("Grass") || pType.Equals("Ground") || pType.Equals("Flying") || pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fighting"))
        {
            if (pType.Equals("Normal") || pType.Equals("Rock") || pType.Equals("Ice") || pType.Equals("Dark") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Poison"))
        {
            if (pType.Equals("Grass") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ground"))
        {
            if (pType.Equals("Fire") || pType.Equals("Electric") || pType.Equals("Poison") || pType.Equals("Rock") || pType.Equals("Steel"))
            {
                return true;
            }

        }
        else if (moveType.Equals("Flying"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Bug") || pType.Equals("Grass"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Psychic"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Poison"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Bug"))
        {
            if (pType.Equals("Psychic") || pType.Equals("Dark") || pType.Equals("Grass"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Rock"))
        {
            if (pType.Equals("Flying") || pType.Equals("Fire") || pType.Equals("Ice") || pType.Equals("Bug"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ghost"))
        {
            if (pType.Equals("Ghost") || pType.Equals("Psychic"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dragon"))
        {
            if (pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dark"))
        {
            if (pType.Equals("Ghost") || pType.Equals("Psychic"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Steel"))
        {
            if (pType.Equals("Ice") || pType.Equals("Rock") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fairy"))
        {
            if (pType.Equals("Dragon") || pType.Equals("Dark") || pType.Equals("Fighting"))
            {
                return true;
            }
        }

        return false;
    }


    static bool isNotEffective(Move m, Pokemon d)
    {
        String moveType = m.Type;
        String pType = d.Type;

        if (moveType.Equals("Water"))
        {
            if (pType.Equals("Water") || pType.Equals("Grass") || pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Grass"))
        {
            if (pType.Equals("Grass") || pType.Equals("Fire") || pType.Equals("Dragon") || pType.Equals("Poison") || pType.Equals("Bug") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fire"))
        {
            if (pType.Equals("Water") || pType.Equals("Fire") || pType.Equals("Dragon") || pType.Equals("Rock"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Electric"))
        {
            if (pType.Equals("Grass") || pType.Equals("Electric") || pType.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ice"))
        {
            if (pType.Equals("Fire") || pType.Equals("Water") || pType.Equals("Ice") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fighting"))
        {
            if (pType.Equals("Poison") || pType.Equals("Flying") || pType.Equals("Psychic") || pType.Equals("Bug") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Poison"))
        {
            if (pType.Equals("Poison") || pType.Equals("Ground") || pType.Equals("Rock") || pType.Equals("Ghost"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ground"))
        {
            if (pType.Equals("Grass") || pType.Equals("Bug"))
            {
                return true;
            }

        }
        else if (moveType.Equals("Flying"))
        {
            if (pType.Equals("Electric") || pType.Equals("Rock") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Psychic"))
        {
            if (pType.Equals("Psychic") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Bug"))
        {
            if (pType.Equals("Fire") || pType.Equals("Fighting") || pType.Equals("Flying") || pType.Equals("Poison") || pType.Equals("Ghost") || pType.Equals("Steel") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Rock"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Ground") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ghost"))
        {
            if (pType.Equals("Dark"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dragon"))
        {
            if (pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Dark"))
        {
            if (pType.Equals("Fighting") || pType.Equals("Dark") || pType.Equals("Fairy"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Steel"))
        {
            if (pType.Equals("Fire") || pType.Equals("Water") || pType.Equals("Electric") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Fairy"))
        {
            if (pType.Equals("Fire") || pType.Equals("Poison") || pType.Equals("Steel"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Normal"))
        {
            if (pType.Equals("Rock") || pType.Equals("Steel"))
            {
                return true;
            }
        }

        return false;
    }

    static bool isImmune(Move m, Pokemon d)
    {
        if (d.Type.Equals("Fairy"))
        {
            if (m.Type.Equals("Dragon"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Ground"))
        {
            if (m.Type.Equals("Electric"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Normal"))
        {
            if (m.Type.Equals("Ghost"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Ghost"))
        {
            if (m.Type.Equals("Normal") || m.Type.Equals("Fighting"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Flying"))
        {
            if (m.Type.Equals("Ground"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Dark"))
        {
            if (m.Type.Equals("Psychic"))
            {
                return true;
            }
        }
        else if (d.Type.Equals("Steel"))
        {
            if (m.Type.Equals("Poison"))
            {
                return true;
            }
        }


        return false;
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
