using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle : MonoBehaviour
{
    private Queue<string> sentences;
    public GameObject a, notifier;
    public GameObject b;
    public List<GameObject> allMyPokemon,allFoePokemon;
    List<Pokemon> deadPokemon = new List<Pokemon>();
    Dictionary<string, int> switchedPokemon = new Dictionary<string, int>();
    public List<Button> buttonColor;
    public Text m1, m2, m3, m4, f1, p1, pcur, pmax, deadText, fText, AMod, DMod, SaMod, SdMod;
    public Slider slider, pSlider;
    public battleText bText,foeText;
    public Text sText,spText;
    bool players;
    bool wasSwitched = true;
    bool checkFainted = true;
    GameObject ActivePokemon;
    GameObject enemyPokemon;
    Pokemon foe;
    Pokemon active;
    Pokemon pastActive;
    ActivePokemon pActive, fActive;
    List<int> foeOrder = new List<int>();
    List<int> playerOrder = new List<int>();
    Move[] activeMoves = new Move[4];
    MoveList moveList = MoveList.GetMoveList();
    PokeDex dex = PokeDex.GetPokeDex();
    Move pMove, fMove;
    int fFainted, pFainted;
    int faintCountF, faintCountP;
    public Image fImage, pImage;
    public Text u1, u2, u3, u4, u5, u6;
    List<Pokemon> playable = new List<Pokemon>();
    GameObject MovePanel, TextPanel, deadPanel, gameOverPanel,switchPanel,switchTextPanel,foeChanged;

    int foeCurrent;
    int playerCurrent;
    // Use this for initialization
    void Start()
    {
        fActive.Player = false;
        pActive.Player = true;
        faintCountF = 0;
        faintCountP = 0;
        System.Random r = new System.Random();
        while (foeOrder.Count != 6)
        {
            int x = r.Next(allFoePokemon.Count);
            if (!foeOrder.Contains(x))
            {
                foeOrder.Add(x);
            }

        }

       

        allFoePokemon[foeOrder[0]].SetActive(true);
        MovePanel = GameObject.FindGameObjectWithTag("movePanel");
        TextPanel = GameObject.FindGameObjectWithTag("textPanel");
        deadPanel = GameObject.FindGameObjectWithTag("deadPanel");
        gameOverPanel = GameObject.FindGameObjectWithTag("gameOver");
        switchPanel = GameObject.FindGameObjectWithTag("switchPanel");
        switchTextPanel = GameObject.FindGameObjectWithTag("switchText");
        foeChanged = GameObject.FindGameObjectWithTag("foeChanged");

        TextPanel.SetActive(false);
        deadPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        switchPanel.SetActive(false);
        switchTextPanel.SetActive(false);
        foeChanged.SetActive(false);
       
        List<Pokemon> pokemon = dex.GetPokemon;
        enemyPokemon = GameObject.FindGameObjectWithTag("Foe");
        String enemyName = enemyPokemon.transform.parent.tag;
     
        foe = findMon(enemyName, pokemon);
        fActive.P = foe;
       
       
       
        foreach(Pokemon p in pokemon)
        {
            if (p.Playable)
            {
                playable.Add(p);
                //switchedPokemon.add;
            }

        }

        while (playerOrder.Count != 6)
        {
            int x = r.Next(playable.Count);
            if (!playerOrder.Contains(x))
            {
                playerOrder.Add(x);
            }

        }
        u1.text = playable[playerOrder[0]].Name;
        u2.text = playable[playerOrder[1]].Name;
        u3.text = playable[playerOrder[2]].Name;
        u4.text = playable[playerOrder[3]].Name;
        u5.text = playable[playerOrder[4]].Name;
        u6.text = playable[playerOrder[5]].Name;

        Debug.Log("Text"+u1.text);
        setNewActive(playable[playerOrder[0]].Name);
        active = findActive();
        pActive.P = active;
        updateMon();
        //Create list of playable Pokemon, choose 6.
        
        fFainted = 0;
        pFainted = 0;
        updateScreen();



        //charmander.Play();
    }

    // Update is called once per frame
    void Update()
    {

        updateScreen();
        //pcur.text = playerCurrent.ToString();

       

        if (notifier.activeSelf)
        {
            notifier.SetActive(false);
            if (checkFainted)
            {
                if (slider.value == 0)
                {

                    checkFainted = false;
                    players = false;
                    pokemonFainted(foe);

                }
                else if (pSlider.value == 0)
                {
                    checkFainted = false;
                    players = true;
                    pokemonFainted(active);
                }
            }
        }
    }

    void updateScreen()
    {
        List<Pokemon> pokemon = dex.GetPokemon;

        //active = findActive();


        int count = 0;
        foreach (Move m in active.Moves)
        {
            activeMoves[count] = m;
            count++;
        }
        count = 0;

        m1.text = activeMoves[0].Name;
        m2.text = activeMoves[1].Name;
        m3.text = activeMoves[2].Name;
        m4.text = activeMoves[3].Name;



        if (wasSwitched)
        {
            enemyPokemon = GameObject.FindGameObjectWithTag("Foe");
           
            String enemyName = enemyPokemon.transform.parent.tag;
            foe = findMon(enemyName, pokemon);
            f1.text = foe.Name;
            fImage.color = Color.green;
            slider.maxValue = foe.Stats[0].HP;
            slider.value = foe.Stats[0].HP;
            foeCurrent = foe.Stats[0].HP;
            wasSwitched = false;
            fActive.P = foe;
        }

    }


    Pokemon findMon(String s, List<Pokemon> list)
    { 

        foreach (Pokemon p in list)
        {
            if (p.Name.Equals(s))
            {
                Pokemon new1 = new Pokemon();
                new1 = p;
                return new1;
            }
        }
        return new Pokemon();

    }

    public void onClickM1()
    {

        Move used = activeMoves[0];
        damageStuff(used);

    }

    public void onClickM2()
    {
        Move used = activeMoves[1];
        damageStuff(used);
    }

    public void onClickM3()
    {
        Move used = activeMoves[2];
        damageStuff(used);
    }

    public void onClickM4()
    {
        Move used = activeMoves[3];
        damageStuff(used);
    }

 

    void damageStuff(Move m)
    {
        //pMove = m;
        Move foemove = foeMove();
        fMove = foemove;
        bool playerfirst = playerFirst();
        TextPanel.SetActive(true);
        int cHP = (int)pSlider.value;
        int fcHp = (int)slider.value;

        Debug.Log("Foe Move: " + foemove.Name);
        Debug.Log("Move: " + m.Name);
        //FindObjectOfType<Agent>().agent();
        FindObjectOfType<TurnSystem>().doStuff(pActive, fActive, foemove, m, bText, playerfirst, cHP, fcHp);



    }

    Pokemon findActive()
    {
        List<Pokemon> pokemon = dex.GetPokemon;

        ActivePokemon = GameObject.FindGameObjectWithTag("Player");
        String pokemonName = ActivePokemon.transform.parent.tag;
        active = findMon(pokemonName, pokemon);
        pActive.P = active;
        return active;

    }

    Move foeMove()
    {

        System.Random r = new System.Random();

        int x = r.Next(4);



        return foe.Moves[x];
    }

    bool playerFirst()
    {
        if (active.Stats[0].Spd > foe.Stats[0].Spd)
        {
            return true;
        }
        else if (foe.Stats[0].Spd > active.Stats[0].Spd)
        {
            return false;
        }
        else
        {
            System.Random rand = new System.Random();
            int x = rand.Next(2);

            if (x == 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }

    void pokemonFainted(Pokemon fainted)
    {

        if (players) {
            deadPokemon.Add(active);
            //set mod to 1
            AMod.text = "1";
            DMod.text = "1";
            SaMod.text = "1";
            SdMod.text = "1";
            blockFainted();
        setSwitchText(fainted);
        }
        else {

            foeFaint(fainted);
}

    }



    void updateMon()
    {
        pImage.color = Color.green;
        p1.text = active.Name;

        playerCurrent = active.Stats[0].HP;
        pcur.text = playerCurrent.ToString();
        pmax.text = playerCurrent.ToString();

        pSlider.maxValue = playerCurrent;
        pSlider.value = playerCurrent;
    }
    public void setSwitchText(Pokemon f)
    {
        
         pFainted = pFainted + 1;

        deadText.text = f.Name + " fainted...";
        if(pFainted == 6)
        {
            
            
            foeText.sentences[0] = f.Name + " fainted...";
            foeText.sentences[1] = "placeholder";
        foreach (string sentence in foeText.sentences)
            {
                sentences.Enqueue(sentence);
            }
            foeChanged.SetActive(true);
            displayNext();
        }
        else { 
        switchPanel.SetActive(true); 
        deadPanel.SetActive(true);}
  
    }
    public void foeFaint(Pokemon f)
    {
        sentences = new Queue<string>();
        fFainted = fFainted + 1;
        foeText.sentences[0] = f.Name+ " fainted...";
        List<Pokemon> pokemon = dex.GetPokemon;
        
        if(fFainted < 6) {
        Pokemon next = findMon(allFoePokemon[foeOrder[fFainted]].tag, pokemon);
        foeText.sentences[1] = "Foe sends out " + next.Name;
        }
        else
        {
            foeText.sentences[1] = "placeholder " ;
        }
        foreach(string sentence in foeText.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foeChanged.SetActive(true);
        GameObject turnOff = enemyPokemon.transform.parent.gameObject;
        turnOff.SetActive(false);
        displayNext();

    }
    public void displayNext()
    {
        if (sentences.Count == 1&&fFainted==6||sentences.Count==1&&pFainted==6)
        {
            gameOverPanel.SetActive(true);
            return;
        }
       if(sentences.Count == 0)
        {
            foeChanged.SetActive(false);
            return;
        }
       else if (sentences.Count == 1)
        {
            allFoePokemon[foeOrder[fFainted]].SetActive(true);
            wasSwitched = true;
            checkFainted = true;
        }
        string sentence = sentences.Dequeue();
        
        fText.text = sentence;

    }

    public void onClickU1()
    {

    }
    public void onClickU2()
    {
        bool setActive=true;
        foreach(Pokemon p in deadPokemon)
        {
            if (p.Name == u2.text)
            {
                setActive = false;
                break;
            }
        }
        if (setActive) {
        String newActive = u2.text;
        
        setNewActive(newActive);
        switching();
         }
    }
    public void onClickU3()
    {
        bool setActive = true;
        foreach (Pokemon p in deadPokemon)
        {
            if (p.Name == u3.text)
            {
                setActive = false;
                break;
            }
        }
        if (setActive)
        {
setNewActive(u3.text);
        switching();
        }
        
    }
    public void onClickU4()
    {
        bool setActive = true;
        foreach (Pokemon p in deadPokemon)
        {
            if (p.Name == u4.text)
            {
                setActive = false;
                break;
            }
        }

        if (setActive)
        {
setNewActive(u4.text);
        switching();
        }
        
    }
    public void onClickU5()
    {
        bool setActive = true;
        foreach (Pokemon p in deadPokemon)
        {
            if (p.Name == u5.text)
            {
                setActive = false;
                break;
            }
        }
        if (setActive)
        {
        setNewActive(u5.text);
        switching();
        }
       
    }
    public void onClickU6()
    {
        bool setActive = true;
        foreach (Pokemon p in deadPokemon)
        {
            if (p.Name == u6.text)
            {
                setActive = false;
                break;
            }
        }
        if (setActive)
        {
        setNewActive(u6.text);
        switching();
        }
       
    }
    void switching()
    {
        GameObject turnOff = ActivePokemon.transform.parent.gameObject;
        turnOff.SetActive(false);
        switchPanel.SetActive(false);
        findActive();
        updateMon();
        deadPanel.SetActive(false);
        checkFainted = true;
    }

    public void spOnClick()
    {
        switchTextPanel.SetActive(false);
    }

    void setNewActive(String s)
    {
        Debug.Log(s);

        s = s.ToUpper();

        foreach(GameObject g in allMyPokemon)
        {
           
            if (g.name.ToUpper().Contains(s))
            {
                g.SetActive(true);
                return;
            }
        }

  
    
    }
    

    void blockFainted()
    {
        foreach(Pokemon p in deadPokemon)
        {
            if (p.Name == u1.text)
            {
                buttonColor[0].image.color = Color.red;
            }
            if(p.Name == u2.text)
            {
                buttonColor[1].image.color = Color.red;
            }
            if(p.Name == u3.text)
            {
                buttonColor[2].image.color = Color.red;
            }
            if(p.Name == u4.text)
            {
                buttonColor[3].image.color = Color.red;
            }
            if(p.Name == u5.text)
            {
                buttonColor[4].image.color = Color.red;
            }
            if(p.Name == u6.text)
            {
                buttonColor[5].image.color = Color.red;
            }
        }
    }
}
