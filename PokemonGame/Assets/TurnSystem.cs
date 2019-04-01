using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour{
    private Queue<string> sentences;
    public Text btext,deadText,hpText;
    public GameObject deadPanel,notifier;
   public Slider fSlider, pSlider;
    public Image fImage, pImage;
    public Button next;
    Pokemon player, foe;
    int cHP,fcHP;
    Move pMove, fMove;
    bool First;
    battleText b;
    PokeDex dex = PokeDex.GetPokeDex();

    public void doStuff(Pokemon a, Pokemon f,Move fmove, Move pmove, battleText battletext,bool first,int cHp,int fcHp)
    {
        b = battletext;
        player = a;
        foe = f;
        cHP = cHp;
        fcHP = fcHp;
        pMove = pmove;
        fMove = fmove;
        First = first;
        sentences = new Queue<string>();
        

        if( fcHP > 0 && cHP > 0) {
            if (pmove.Info.Equals("First") && !(fmove.Info.Equals("First"))){
                first = true;
            }
            else if (fmove.Info.Equals("First") && !(pmove.Info.Equals("First"))){
                first = false;
            }
        if (first)
        {
            damageCalc(pMove, player, foe);
            updateText(player, foe, pmove, fmove, battletext);
         }
        else
        {
            damageCalc(fMove, foe, player);
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
        
        if (sentences.Count < 2) {
         if (cHP <= 0)
        {
                notifier.SetActive(true);
                EndDialogue();
           
        }
        else if(fcHP <= 0)
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
                    damageCalc(fMove, foe, player);
                }
            }
            else
            {
                
                if(cHP>0 && fcHP> 0) { 
                damageCalc(pMove, player, foe);}
            }
        }
        if (sentences.Count == 0)
        {

            EndDialogue();

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
        }catch(Exception e){

        }
        
        //Turn off move panel
    }

    void damageCalc(Move m, Pokemon a, Pokemon d)
    {
        //Calculate modifier
        

        double mod=1;
        if (isSuperEffective(m, d))
        {
            mod = mod*2;
        }
        if (isNotEffective(m, d))
        {
            mod = mod * 0.5;
        }
        if (isImmune(m, d))
        {
            mod = mod * 0;
        }
        if (a.Type.Equals(m.Type))
        {
            mod = mod*1.5;
        }

        //Damage  = ((((2*LVL)/5) +2) * Power *Attack/Defense) /50) +2
        if (!m.Info.Equals("Heal"))
        {
            int typeA=0;
            int typeD=0;
            if (m.Attack.Equals("S"))
            {
                typeA = a.Stats[0].SpAtk;
                typeD = d.Stats[0].SpDef;
            }
            else if (m.Attack.Equals("P"))
            {
                typeA = a.Stats[0].Atk;
                typeD = d.Stats[0].Def;
            }
        double dam = ((((((2 * 50) / 5) + 2) * m.Power * typeA / typeD) / 50) + 2) * mod;
        int damage = (int)dam;
            if(m.Name.Equals("Sonic Boom"))
            {
                damage = 20;
            }
            Debug.Log(damage);
        if (a.Equals(player))
        {
                if (m.Info.Equals("Drain"))
                {
                    cHP = cHP + (damage / 2);

                    if(cHP > a.Stats[0].HP)
                    {
                        cHP = a.Stats[0].HP;
                    }
                    pSlider.value = cHP;
                    sliderUpdate(pSlider, pImage,a);

                }
            fcHP = fcHP - damage;
            if(fcHP < 0)
            {
                fcHP = 0;
            }
            fSlider.value = fcHP;
                sliderUpdate(fSlider, fImage, d);
            
        }
        else
        {

                if (m.Info.Equals("Drain"))
                {
                    fcHP = fcHP + (damage / 2);

                    if (fcHP > a.Stats[0].HP)
                    {
                        fcHP = a.Stats[0].HP;
                    }
                    fSlider.value = fcHP;
                    sliderUpdate(fSlider, fImage, a);

                }
                cHP = cHP - damage;
            if(cHP < 0)
            {
                cHP = 0;
            }
           
            pSlider.value = cHP;


                sliderUpdate(pSlider, pImage, d);
        }
}
        else
        {
            if (a.Equals(player))
            {
                cHP = cHP + (a.Stats[0].HP / 2);
                if (cHP > a.Stats[0].HP)
                {
                    cHP = a.Stats[0].HP;
                }
                sliderUpdate(pSlider, pImage, a);
            }
            else
            {
                fcHP = fcHP + (a.Stats[0].HP / 2);
                if (fcHP > a.Stats[0].HP)
                {
                    fcHP = a.Stats[0].HP;
                }
                sliderUpdate(fSlider, fImage, a);
            }
        }
        hpText.text = cHP.ToString();
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
            if(pType.Equals("Normal")|| pType.Equals("Rock") || pType.Equals("Ice") || pType.Equals("Dark") || pType.Equals("Steel"))
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
        else if(moveType.Equals("Ground"))
        {
            if (pType.Equals("Fire") || pType.Equals("Electric") || pType.Equals("Poison") || pType.Equals("Rock") || pType.Equals("Steel"))
            {
                return true;
            }
          
        }
        else if (moveType.Equals("Flying"))
        {
            if (pType.Equals("Fighting")  || pType.Equals("Bug") || pType.Equals("Grass"))
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
            if (pType.Equals("Flying") || pType.Equals("Fire") || pType.Equals("Ice")||pType.Equals("Bug"))
            {
                return true;
            }
        }
        else if (moveType.Equals("Ghost"))
        {
            if (pType.Equals("Ghost") || pType.Equals("Psychic") )
            {
                return true;
            }
        }
        else if (moveType.Equals("Dragon"))
        {
            if (pType.Equals("Dragon") )
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
            if (pType.Equals("Grass") || pType.Equals("Fire") || pType.Equals("Dragon")||pType.Equals("Poison") || pType.Equals("Bug") || pType.Equals("Steel"))
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
            if (pType.Equals("Grass") || pType.Equals("Bug") )
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

    bool isImmune(Move m,Pokemon d)
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
            if (m.Type.Equals("Normal")||m.Type.Equals("Fighting"))
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

    void updateText(Pokemon a,Pokemon d,Move m,Move n,battleText battletext)
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
        {
            b.sentences[0] = a.Name + " used " + m.Name;
        }
        if (isSuperEffective(n, a))
        {
            b.sentences[1] = d.Name + " used " + n.Name + ". It was Super Effective!";
        }
        else if (isNotEffective(n, a))
        {
            b.sentences[1] = d.Name + " used " +n.Name + ". It's not very effective!";
        }
        else if (isImmune(n, a))
        {
            b.sentences[1] = d.Name + " used" + n.Name + ". It had no effect.";
        }
        else
        {
            b.sentences[1] = d.Name + " used " + n.Name;
        }
    }


    void sliderUpdate(Slider s, Image c,Pokemon p)
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
}
