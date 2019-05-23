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
    Stat agentModifier = new Stat(0, 1, 1, 1, 1);
    Stat playerModifier = new Stat(0, 1, 1, 1, 1);
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
        pMove = pmove;

        agentModifier.hp = fcHp;
        playerModifier.hp = cHp;

        First = first;
        if (!previousA.Equals(pActive))
        {
            playerModifier.attack = 1;
            playerModifier.defence = 1;
            playerModifier.specialAttack = 1;
            playerModifier.specialDefence = 1;
        }
        if (!previousF.Equals(fActive))
        {
            agentModifier.attack = 1;
            agentModifier.defence = 1;
            agentModifier.specialAttack = 1;
            agentModifier.specialAttack = 1;
        }
        Stat pstats = playerModifier.Copy();
        Stat fstats = agentModifier.Copy();
        Agent _Agent = new Agent();
        Move m = _Agent.agent(a, f, pstats, fstats);
        fMove = m;
        fmove = m;
        AMod.text = playerModifier.attack.ToString();
        DMod.text = playerModifier.ToString();
        SaMod.text = playerModifier.specialAttack.ToString();
        SdMod.text = playerModifier.specialDefence.ToString();
        sentences = new Queue<string>();

        if (agentModifier.hp > 0 && playerModifier.hp > 0)
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
            if (playerModifier.hp <= 0)
            {
                notifier.SetActive(true);
                EndDialogue();

            }
            else if (agentModifier.hp <= 0)
            {
                notifier.SetActive(true);
                EndDialogue();

            }
        }

        if (sentences.Count == 1)
        {

            if (First)
            {
                if (playerModifier.hp > 0 && agentModifier.hp > 0)
                {
                    First = !First;
                    damageCalc(fMove, fActive, pActive);
                }
            }
            else
            {

                if (playerModifier.hp > 0 && agentModifier.hp > 0)
                {
                    First = !First;
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


        List<Stat> result;
        if (First)
        {

            result = DamageCalculator.calc(m, a, d, playerModifier, agentModifier);
            playerModifier = result[0].Copy();
            agentModifier = result[1].Copy();

            pSlider.value = (float)playerModifier.hp;
            sliderUpdate(pSlider, pImage, a.P);

            fSlider.value = (float)agentModifier.hp;
            sliderUpdate(fSlider, fImage, d.P);
        }
        else
        {
            result = DamageCalculator.calc(m, a, d, agentModifier, playerModifier);
            playerModifier = result[1].Copy();
            agentModifier = result[0].Copy();

            fSlider.value = (float)agentModifier.hp;
            sliderUpdate(fSlider, fImage, a.P);

            pSlider.value = (float)playerModifier.hp;
            sliderUpdate(pSlider, pImage, d.P);

        }





        hpText.text = playerModifier.hp.ToString();
        AMod.text = playerModifier.attack.ToString();
        DMod.text = playerModifier.defence.ToString();
        SaMod.text = playerModifier.specialAttack.ToString();
        SdMod.text = playerModifier.specialDefence.ToString();
    }





    void updateText(Pokemon a, Pokemon d, Move m, Move n, battleText battletext)
    {
        TypeCheck type = new TypeCheck();

        Debug.Log(m.Name + " " + n.Name);
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
            b.sentences[1] = d.Name + " used " + n.Name + ". It had no effect.";
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

        switch (m.Info)
        {
            

            case Move.InfoType.ATTACK_UP_DEFENCE_UP:
                b.sentences[i] = a.Name + " used " + m.Name + ".It's Attack and Defense rose!";

                break;

            case Move.InfoType.SPECIAL_ATTACK_UP_SPECIAL_DEFENCE_UP:
                b.sentences[i] = a.Name + " used " + m.Name + ".It's Special Attack and Special Defense rose!";

                break;

            case Move.InfoType.SPECIAL_ATTACK_UP_2:
                b.sentences[i] = a.Name + " used " + m.Name + ".It's Special Attack rose sharply!";

                break;

            case Move.InfoType.HEAL:
                b.sentences[i] = a.Name + " used " + m.Name + ".It restored it's health!";

                break;

            case Move.InfoType.DEFENCE_UP:
                b.sentences[i] = a.Name + " used " + m.Name + ".It's Defence rose!";

                break;

            default:
                b.sentences[i] = a.Name + " used " + m.Name;

                break;
        }

    }

}

   

