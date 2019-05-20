using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    private Queue<string> sentences;
    public Text btext, deadText, hpText, AMod, DMod, SaMod, SdMod;
    public GameObject deadPanel, notifier;
    public Slider fSlider, pSlider;
    public Image fImage, pImage;
    public Button next;
    ActivePokemon fActive, pActive;
    Pokemon player, foe;
    int cHP, fcHP;
    double aM = 1, dM = 1, spaM = 1, spdM = 1, faM = 1, fdM = 1, fspaM = 1, fspdM = 1;
    Move pMove, fMove;
    bool First;
    battleText b;
    PokeDex dex = PokeDex.GetPokeDex();
    ActivePokemon previousA, previousF;


    public void doStuff(ActivePokemon a, ActivePokemon f, Move fmove, Move pmove, battleText battletext, bool first, int cHp, int fcHp)
    {
        fActive = f;
        pActive = a;
        b = battletext;
        player = a.P;
        foe = f.P;
        cHP = cHp;
        fcHP = fcHp;
        pMove = pmove;



        First = first;
        if (!previousA.Equals(pActive))
        {
            aM = 1;
            dM = 1;
            spaM = 1;
            spdM = 1;
        }
        if (!previousF.Equals(fActive))
        {
            faM = 1;
            fdM = 1;
            fspaM = 1;
            fspdM = 1;
        }
        double[] pstats = { cHP, aM, dM, spaM, spdM };
        double[] fstats = { fcHP, faM, fdM, fspaM, fspdM };
        Agent _Agent = new Agent();
        Move m = _Agent.agent(a, f, pstats, fstats);
        fMove = m;
        fmove = m;
        Debug.Log(fmove.Name);
        Debug.Log(pmove.Name);
        // Debug.Log(fMove.Name);
        // Debug.Log("Chosen Move ===" + m.Name);
        AMod.text = aM.ToString();
        DMod.text = dM.ToString();
        SaMod.text = spaM.ToString();
        SdMod.text = spdM.ToString();
        sentences = new Queue<string>();
        Debug.Log(pmove.Info.Equals("First"));
        Debug.Log(fmove.Info.Equals("First"));
        if (fcHP > 0 && cHP > 0)
        {
            if (pmove.Info.Equals("First") && !(fmove.Info.Equals("First")))
            {
                first = true;
            }
            else if (fmove.Info.Equals("First") && !(pmove.Info.Equals("First")))
            {
                first = false;
            }
            First = first;
            if (first)
            {
                damageCalc(pMove, a, f);
                updateText(player, foe, pmove, fmove, battletext);
            }
            else
            {
                damageCalc(fMove, f, a);
                updateText(foe, player, fmove, pmove, battletext);
            }
        }

        foreach (string sentence in b.sentences)
        {
            sentences.Enqueue(sentence);
        }


        DisplayNextSentence();


    }

    public void DisplayNextSentence()
    {

        if (sentences.Count < 2)
        {
            if (cHP <= 0)
            {
                notifier.SetActive(true);
                EndDialogue();

            }
            else if (fcHP <= 0)
            {
                notifier.SetActive(true);
                EndDialogue();

            }
        }

        if (sentences.Count == 1)
        {

            if (First)
            {
                if (cHP > 0 && fcHP > 0)
                {
                    damageCalc(fMove, fActive, pActive);
                }
            }
            else
            {

                if (cHP > 0 && fcHP > 0)
                {
                    damageCalc(pMove, pActive, fActive);
                }
            }
        }
        if (sentences.Count == 0)
        {

            EndDialogue();
            sentences.Clear();
            return;
        }

        string sentence = sentences.Dequeue();
        btext.text = sentence;







    }



    void EndDialogue()
    {
        try
        {
            GameObject textPanel = GameObject.FindGameObjectWithTag("textPanel");
            textPanel.SetActive(false);
            sentences.Clear();

        }
        catch (Exception e)
        {

        }

        //Turn off move panel
    }

    void damageCalc(Move m, ActivePokemon a, ActivePokemon d)
    {
        previousA = pActive;
        previousF = fActive;
        //Calculate modifier


        double mod = 1;
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

        //Damage  = ((((2*LVL)/5) +2) * Power *Attack/Defense) /50) +2
        if (!m.Info.Equals("Heal") && !m.Type.Equals("Null"))
        {
            double typeA = 0;
            double typeD = 0;
            if (m.Attack.Equals("S"))
            {
                //Debug.Log("User Modifier: " + spaM);
                //Debug.Log("Foe Modifier: " + fspaM);
                if (First)
                {
                    typeA = a.P.Stats[0].SpAtk * spaM;
                    typeD = d.P.Stats[0].SpDef * fspdM;
                }
                else
                {
                    typeA = a.P.Stats[0].SpAtk * fspaM;
                    typeD = d.P.Stats[0].SpDef * spdM;
                }

            }
            else if (m.Attack.Equals("P"))
            {
                // Debug.Log("User Modifier: " + aM);
                // Debug.Log("Foe Modifier: " + faM);
                if (First)
                {
                    typeA = a.P.Stats[0].Atk * aM;
                    typeD = d.P.Stats[0].Def * fdM;
                }
                else
                {
                    typeA = a.P.Stats[0].Atk * faM;
                    typeD = d.P.Stats[0].Def * dM;
                }

            }
            double dam = ((((((2 * 50) / 5) + 2) * m.Power * typeA / typeD) / 50) + 2) * mod;
            int damage = (int)dam;
            if (m.Name.Equals("Sonic Boom"))
            {
                damage = 20;
            }
            if (a.Equals(pActive))
            {
                if (m.Info.Equals("Drain"))
                {
                    cHP = cHP + (damage / 2);

                    if (cHP > a.P.Stats[0].HP)
                    {
                        cHP = a.P.Stats[0].HP;
                    }
                    pSlider.value = cHP;
                    sliderUpdate(pSlider, pImage, a.P);

                }
                //AUp, DDownSpDown,iw
                else if (m.Info.Equals("AUp"))
                {
                    aM = modIncrease(aM, 1);
                }
                else if (m.Info.Equals("DDownSpDDown"))
                {
                    dM = modDecrease(dM, 1);
                    spdM = modDecrease(spdM, 1);
                    //Debug.Log(dM + "," + spdM);
                }

                fcHP = fcHP - damage;
                if (fcHP < 0)
                {
                    fcHP = 0;
                }
                fSlider.value = fcHP;
                sliderUpdate(fSlider, fImage, d.P);

            }
            else
            {

                if (m.Info.Equals("Drain"))
                {
                    fcHP = fcHP + (damage / 2);

                    if (fcHP > a.P.Stats[0].HP)
                    {
                        fcHP = a.P.Stats[0].HP;
                    }
                    fSlider.value = fcHP;
                    sliderUpdate(fSlider, fImage, a.P);

                }
                else if (m.Info.Equals("AUp"))
                {
                    faM = modIncrease(faM, 1);
                }
                else if (m.Info.Equals("DDownSpDDown"))
                {
                    fdM = modDecrease(fdM, 1);
                    fspdM = modDecrease(fspdM, 1);
                }
                cHP = cHP - damage;
                if (cHP < 0)
                {
                    cHP = 0;
                }

                pSlider.value = cHP;


                sliderUpdate(pSlider, pImage, d.P);
            }


        }
        //
        else if (m.Info.Equals("Heal"))
        {
            if (a.Equals(pActive))
            {
                cHP = cHP + (a.P.Stats[0].HP / 2);
                if (cHP > a.P.Stats[0].HP)
                {
                    cHP = a.P.Stats[0].HP;
                }
                pSlider.value = cHP;
                sliderUpdate(pSlider, pImage, a.P);
            }
            else
            {
                // Debug.Log("Foe recovery test");
                //  Debug.Log(fcHP);
                fcHP = fcHP + (a.P.Stats[0].HP / 2);
                // Debug.Log(fcHP);
                if (fcHP > a.P.Stats[0].HP)
                {
                    fcHP = a.P.Stats[0].HP;
                }
                // Debug.Log(fcHP);
                fSlider.value = fcHP;
                sliderUpdate(fSlider, fImage, a.P);
            }
        }
        else
        {
            //Debug.Log(First);
            //  Debug.Log("Stat Move");
            if (m.Info.Equals("AUpDUp"))
            {
                if (a.Equals(pActive))
                {
                    aM = modIncrease(aM, 1);
                    dM = modIncrease(dM, 1);
                }
                else
                {
                    faM = modIncrease(faM, 1);
                    fdM = modIncrease(fdM, 1);
                }
            }
            else if (m.Info.Equals("SpAUpSpDUp"))
            {
                if (a.Equals(pActive))
                {
                    spaM = modIncrease(spaM, 1);
                    spdM = modIncrease(spdM, 1);
                }
                else
                {
                    fspaM = modIncrease(fspaM, 1);
                    fspdM = modIncrease(fspdM, 1);
                }
            }
            else if (m.Info.Equals("SpAUp2"))
            {
                //  Debug.Log("Nasty Plot!");
                if (a.Equals(pActive))
                {
                    spaM = modIncrease(spaM, 2);
                }
                else
                {
                    fspaM = modIncrease(fspaM, 2);

                }

            }
            else
            {
                //  Debug.Log("Bleh");
            }
            AMod.text = aM.ToString();
            DMod.text = dM.ToString();
            SaMod.text = spaM.ToString();
            SdMod.text = spdM.ToString();

        }
        hpText.text = cHP.ToString();

        AMod.text = aM.ToString();
        DMod.text = dM.ToString();
        SaMod.text = spaM.ToString();
        SdMod.text = spdM.ToString();
    }

    bool isSuperEffective(Move m, Pokemon d)
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


    bool isNotEffective(Move m, Pokemon d)
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

    bool isImmune(Move m, Pokemon d)
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

    void updateText(Pokemon a, Pokemon d, Move m, Move n, battleText battletext)
    {
        if (isSuperEffective(m, d))
        {
            b.sentences[0] = a.Name + " used " + m.Name + ". It's Super Effective!";
        }
        else if (isNotEffective(m, d))
        {
            b.sentences[0] = a.Name + " used " + m.Name + ". It's not very effective!";
        }
        else if (isImmune(m, d))
        {
            b.sentences[0] = a.Name + " used " + m.Name + ". It had no effect.";
        }
        else
        { ///DDownSpDDown AUpDUp SpAUpSpDUp SpAUp2
            extraText(m, a, 0);
        }
        if (isSuperEffective(n, a))
        {
            b.sentences[1] = d.Name + " used " + n.Name + ". It was Super Effective!";
        }
        else if (isNotEffective(n, a))
        {
            b.sentences[1] = d.Name + " used " + n.Name + ". It's not very effective!";
        }
        else if (isImmune(n, a))
        {
            b.sentences[1] = d.Name + " used" + n.Name + ". It had no effect.";
        }
        else
        {
            extraText(n, d, 1);
        }
    }


    void sliderUpdate(Slider s, Image c, Pokemon p)
    {
        if (s.value <= p.Stats[0].HP / 4)
        {
            c.color = Color.red;
        }
        else if (s.value <= p.Stats[0].HP / 2)
        {
            c.color = Color.yellow;
        }
        else
        {
            c.color = Color.green;
        }
    }

    void extraText(Move m, Pokemon a, int i)
    {
        if (m.Info.Equals("DDownSpDDown"))
        {
            b.sentences[i] = a.Name + " used" + m.Name + ".It's Defense and Special Defense fell!";
        }
        else if (m.Info.Equals("AUpDUp"))
        {
            b.sentences[i] = a.Name + " used" + m.Name + ".It's Attack and Defense rose!";

        }
        else if (m.Info.Equals("SpAUpSpDUp"))
        {
            b.sentences[i] = a.Name + " used" + m.Name + ".It's Special Attack and Special Defense rose!";

        }
        else if (m.Info.Equals("SpAUp2"))
        {
            b.sentences[i] = a.Name + " used" + m.Name + ".It's Special Attack rose sharply!";

        }
        b.sentences[i] = a.Name + " used " + m.Name;
    }

    double modIncrease(double modd, int stages)
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

    double modDecrease(double modd, int stages)
    {
        // Debug.Log("Decreasing from "+modd);

        do
        {
            if (modd > 1)
            {
                modd = modd - 0.5;

            }
            else if (modd > 0.25 && modd <= 1)
            {
                // Debug.Log("Standard decrease");
                modd = modDown(modd);
                //  Debug.Log("Returned Value: " + modd);
            }

            stages = stages - 1;
        } while (stages != 0);

        // Debug.Log(modd);
        return modd;
    }

    double modDown(double modd)
    {
        // Debug.Log("Down");
        // Debug.Log(modd);

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
        // Debug.Log(modd);
        return modd;
    }

    double modUp(double modd)
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
