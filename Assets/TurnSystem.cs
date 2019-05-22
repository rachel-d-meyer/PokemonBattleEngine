using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;

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
        Stat pstats = new Stat(cHP, aM, dM, spaM, spdM);
        Stat fstats = new Stat(fcHP, faM, fdM, fspaM, fspdM);
        Agent _Agent = new Agent();
        Move m = _Agent.agent(a, f, pstats, fstats);
        fMove = m;
        fmove = m;
        AMod.text = aM.ToString();
        DMod.text = dM.ToString();
        SaMod.text = spaM.ToString();
        SdMod.text = spdM.ToString();
        sentences = new Queue<string>();

        if (fcHP > 0 && cHP > 0)
        {
            if (pmove.Info == Move.InfoType.FIRST && fmove.Info != Move.InfoType.FIRST)
            {
                first = true;
            }
            else if (fmove.Info == Move.InfoType.FIRST && pmove.Info != Move.InfoType.FIRST)
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
            //Debug.Log(e.Message);
        }

    }


    void damageCalc(Move m, ActivePokemon a, ActivePokemon d)
    {
        TypeCheck type = new TypeCheck();
        previousA = pActive;
        previousF = fActive;

        double mod = 1;
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
             
                if (First)
                {
                    typeA = a.P.Stats[0].Atk * aM;
                    typeD = d.P.Stats[0].Def * fdM;
                    if (m.Info == Move.InfoType.FOE)
                    {
                        typeA = d.P.Stats[0].Atk * faM;
                    }
                }
                else
                {
                    typeA = a.P.Stats[0].Atk * faM;
                    typeD = d.P.Stats[0].Def * dM;
                    if (m.Info == Move.InfoType.FOE)
                    {
                        typeA = a.P.Stats[0].Atk * aM;
                    }
                }

            }
            double dam = ((((((2 * 50) / 5) + 2) * m.Power * typeA / typeD) / 50) + 2) * mod;
            int damage = (int)dam;
            if (m.Name.Equals("Sonic Boom"))
            {
                damage = 20;
            }
            if(m.Name.Equals("Seismic Toss"))
            {
                damage = 50;
            }
            if (a.Equals(pActive))
            {
                switch (m.Info)
                {
                    case Move.InfoType.DRAIN:
                        cHP = cHP + (damage / 2);
                        if (cHP > a.P.Stats[0].HP)
                        {
                            cHP = a.P.Stats[0].HP;
                        }
                        pSlider.value = cHP;
                        sliderUpdate(pSlider, pImage, a.P);
                        break;

                    case Move.InfoType.LEECH:
                        cHP = cHP + (int)(damage / 1.5);
                        if (cHP > a.P.Stats[0].HP)
                        {
                            cHP = a.P.Stats[0].HP;
                        }
                        pSlider.value = cHP;
                        sliderUpdate(pSlider, pImage, a.P);
                        break;

                    case Move.InfoType.ATTACK_UP:
                        aM = modIncrease(aM, 1);
                        break;

                    case Move.InfoType.DEFENCE_DOWN_SPECIAL_DEFENCE_DOWN:
                        dM = modDecrease(dM, 1);
                        spdM = modDecrease(spdM, 1);
                        break;

                    case Move.InfoType.FOE_SPECIAL_ATTACK_DOWN:
                        fspaM = modDecrease(fspaM, 1);
                        break;
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
                switch (m.Info)
                {
                    case Move.InfoType.DRAIN:
                        fcHP = fcHP + (damage / 2);
                        if (fcHP > a.P.Stats[0].HP)
                        {
                            fcHP = a.P.Stats[0].HP;
                        }
                        fSlider.value = fcHP;
                        sliderUpdate(fSlider, fImage, a.P);
                        break;

                    case Move.InfoType.LEECH:
                        fcHP = fcHP + (int)(damage / 1.5);
                        if (fcHP > a.P.Stats[0].HP)
                        {
                            fcHP = a.P.Stats[0].HP;
                        }
                        fSlider.value = fcHP;
                        sliderUpdate(fSlider, fImage, a.P);
                        break;

                    case Move.InfoType.ATTACK_UP:
                        faM = modIncrease(faM, 1);
                        break;

                    case Move.InfoType.DEFENCE_DOWN_SPECIAL_DEFENCE_DOWN:
                        fdM = modDecrease(fdM, 1);
                        fspdM = modDecrease(fspdM, 1);
                        break;

                    case Move.InfoType.FOE_SPECIAL_ATTACK_DOWN:
                        spaM = modDecrease(spaM, 1);
                        break;
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
       
        else if (m.Info == Move.InfoType.HEAL)
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
               
                fcHP = fcHP + (a.P.Stats[0].HP / 2);
                if (fcHP > a.P.Stats[0].HP)
                {
                    fcHP = a.P.Stats[0].HP;
                }
                fSlider.value = fcHP;
                sliderUpdate(fSlider, fImage, a.P);
            }
        }
        else if (m.Info == Move.InfoType.ATTACK_UP_2)
        {
            if (a.Equals(pActive))
            {
                aM = modIncrease(aM, 2);
            }
            else
            {
                faM = modIncrease(faM, 2);
            }
        }
        else if (m.Info == Move.InfoType.DEFENCE_UP)
        {
            if (a.Equals(pActive))
            {
                dM = modIncrease(dM, 1);
            }
            else
            {
                fdM = modIncrease(fdM, 2);
            }
        }
        else
        {
            if (m.Info == Move.InfoType.ATTACK_UP_DEFENCE_UP)
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
            else if (m.Info == Move.InfoType.SPECIAL_ATTACK_UP_SPECIAL_DEFENCE_UP)
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
            else if (m.Info == Move.InfoType.SPECIAL_ATTACK_UP_2)
            {
                if (a.Equals(pActive))
                {
                    spaM = modIncrease(spaM, 2);
                }
                else
                {
                    fspaM = modIncrease(fspaM, 2);

                }

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

    

   
    
    void updateText(Pokemon a, Pokemon d, Move m, Move n, battleText battletext)
    {
        TypeCheck type = new TypeCheck();
        if (type.isSuperEffective(m.Type, d))
        {
            b.sentences[0] = a.Name + " used " + m.Name + ". It's Super Effective!";
        }
        else if (type.isNotEffective(m.Type, d))
        {
            b.sentences[0] = a.Name + " used " + m.Name + ". It's not very effective!";
        }
        else if (type.isImmune(m.Type, d))
        {
            b.sentences[0] = a.Name + " used " + m.Name + ". It had no effect.";
        }
        else
        { 
            extraText(m, a, 0);
        }
        if (type.isSuperEffective(n.Type, a))
        {
            b.sentences[1] = d.Name + " used " + n.Name + ". It was Super Effective!";
        }
        else if (type.isNotEffective(n.Type, a))
        {
            b.sentences[1] = d.Name + " used " + n.Name + ". It's not very effective!";
        }
        else if (type.isImmune(n.Type, a))
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
        if (m.Info == Move.InfoType.DEFENCE_DOWN_SPECIAL_DEFENCE_DOWN)
        {
            b.sentences[i] = a.Name + " used" + m.Name + ".It's Defense and Special Defense fell!";
        }
        else if (m.Info == Move.InfoType.ATTACK_UP_DEFENCE_UP)
        {
            b.sentences[i] = a.Name + " used" + m.Name + ".It's Attack and Defense rose!";

        }
        else if (m.Info == Move.InfoType.SPECIAL_ATTACK_UP_SPECIAL_DEFENCE_UP)
        {
            b.sentences[i] = a.Name + " used" + m.Name + ".It's Special Attack and Special Defense rose!";

        }
        else if (m.Info == Move.InfoType.SPECIAL_ATTACK_UP_2)
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

    double modDown(double modd)
    {
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
