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
    public Button b1, b2, b3, b4, b5, b6;
    public List<GameObject> allMyPokemon, allFoePokemon;
    List<Pokemon> deadPokemon = new List<Pokemon>();
    Dictionary<string, int> switchedPokemon = new Dictionary<string, int>();
    public List<Button> buttonColor;
    public Text m1, m2, m3, m4, f1, p1, pcur, pmax, deadText, fText, AMod, DMod, SaMod, SdMod;
    public Slider slider, pSlider;
    public battleText bText, foeText;
    public Text sText, spText;
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
    GameObject MovePanel, TextPanel, deadPanel, gameOverPanel, switchPanel, switchTextPanel, foeChanged;
    List<Pokemon> faintedAgent = new List<Pokemon>();
    Pokemon next = new Pokemon();
    GameObject foeActiveObj;
    public RawImage switch0, switch1, switch2, switch3, switch4, switch5;
    public List<RawImage> agentBalls = new List<RawImage>();
    public List<RawImage> playerBalls = new List<RawImage>();
    public List<Texture> balls = new List<Texture>();
    public List<Texture> sprites = new List<Texture>();
    public Dictionary<string, Texture> spriteList = new Dictionary<string, Texture>();
    int foeCurrent;
    int playerCurrent;


    public void Start()
    {
        //Create a list of sprites used to populate the buttons for switching Pokemon.
        spriteSort sorty = new spriteSort();
        spriteList = sorty.listCreator(sprites);

        //Denote who is the player, and who is the agent.
        fActive.Player = false;
        pActive.Player = true;

        faintCountF = 0;
        faintCountP = 0;

        System.Random r = new System.Random();

        //Randomise the agents Pokemon for this battle
        while (foeOrder.Count != 6)
        {
            int x = r.Next(allFoePokemon.Count);
            if (!foeOrder.Contains(x))
            {
                foeOrder.Add(x);
            }

        }

        //Set the agents first Pokemon as active.
        allFoePokemon[foeOrder[0]].SetActive(true);

        //Find all panels, set them inactive.
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

        //Pull up the full Pokedex, find which Pokemon the foe is.
        List<Pokemon> pokemon = dex.GetPokemon;
        enemyPokemon = GameObject.FindGameObjectWithTag("Foe");
        String enemyName = enemyPokemon.transform.parent.tag;

        foe = findMon(enemyName, pokemon);
        fActive.P = foe;


        //Randomise the players 6 Pokemon dor this battle
        foreach (Pokemon p in pokemon)
        {
            playable.Add(p);
        }

        while (playerOrder.Count != 6)
        {
            int x = r.Next(playable.Count);
            if (!playerOrder.Contains(x))
            {
                playerOrder.Add(x);
            }

        }

        //Set the name of each Pokemon to the corresponding switch button.
        u1.text = playable[playerOrder[0]].Name;
        u2.text = playable[playerOrder[1]].Name;
        u3.text = playable[playerOrder[2]].Name;
        u4.text = playable[playerOrder[3]].Name;
        u5.text = playable[playerOrder[4]].Name;
        u6.text = playable[playerOrder[5]].Name;

        String pText = u1.text;
        //Set the mini-sprite of each Pokemon to the corresponding switch button.
        if (spriteList.ContainsKey(pText))
        {
            switch0.texture = spriteList[pText];
        }

        if (spriteList.ContainsKey(u2.text))
        {
            switch1.texture = spriteList[u2.text];
        }

        if (spriteList.ContainsKey(u3.text))
        {
            switch2.texture = spriteList[u3.text];
        }

        if (spriteList.ContainsKey(u4.text))
        {
            switch3.texture = spriteList[u4.text];
        }
        if (spriteList.ContainsKey(u5.text))
        {
            switch4.texture = spriteList[u5.text];
        }
        if (spriteList.ContainsKey(u6.text))
        {
            switch5.texture = spriteList[u6.text];
        }

        //Set the players first Pokemon to active
        setNewActive(playable[playerOrder[0]].Name);
        active = findActive();
        pActive.P = active;
        updateMon();
        

        fFainted = 0;
        pFainted = 0;
        updateScreen();
    }


    //If the battle is restarted, reset all values to default.
    public void Restart()
    {
        AMod.text = "1";
        DMod.text = "1";
        SaMod.text = "1";
        SdMod.text = "1";

        foreach (RawImage raw in agentBalls)
        {
            raw.texture = balls[1];
        }
        foreach (RawImage raw in playerBalls)
        {
            raw.texture = balls[1];
        }

        b1.GetComponent<RawImage>().color = Color.white;
        b2.GetComponent<RawImage>().color = Color.white;
        b3.GetComponent<RawImage>().color = Color.white;
        b4.GetComponent<RawImage>().color = Color.white;
        b5.GetComponent<RawImage>().color = Color.white;
        b6.GetComponent<RawImage>().color = Color.white;

        wasSwitched = true;
        checkFainted = true;

        switchPanel.SetActive(false);
        deadPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        foeChanged.SetActive(false);
        gameOverPanel.SetActive(false);

        GameObject turnOff = enemyPokemon.transform.parent.gameObject;
        turnOff.SetActive(false);
        turnOff = ActivePokemon.transform.parent.gameObject;
        turnOff.SetActive(false);

        deadPokemon.Clear();
        faintedAgent.Clear();
        foeOrder.Clear();
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
        enemyPokemon = GameObject.FindGameObjectWithTag("Foe");
        String enemyName = enemyPokemon.transform.parent.tag;
        List<Pokemon> pokemon = dex.GetPokemon;
        foe = findMon(enemyName, pokemon);
        fActive.P = foe;

        playerOrder.Clear();
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

        String pText = u1.text;
        if (spriteList.ContainsKey(pText))
        {
            switch0.texture = spriteList[pText];
        }

        if (spriteList.ContainsKey(u2.text))
        {
            switch1.texture = spriteList[u2.text];
        }

        if (spriteList.ContainsKey(u3.text))
        {
            switch2.texture = spriteList[u3.text];
        }

        if (spriteList.ContainsKey(u4.text))
        {
            switch3.texture = spriteList[u4.text];
        }
        if (spriteList.ContainsKey(u5.text))
        {
            switch4.texture = spriteList[u5.text];
        }
        if (spriteList.ContainsKey(u6.text))
        {
            switch5.texture = spriteList[u6.text];
        }

        setNewActive(playable[playerOrder[0]].Name);
        active = findActive();
        pActive.P = active;
        updateMon();

        fFainted = 0;
        pFainted = 0;
        updateScreen();
    }


    void Update()
    {

        updateScreen();

        //Check if any Pokemon have fainted since last turn.
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


    //Update the UI
    void updateScreen()
    {

        PokeDex test = PokeDex.GetPokeDex();
        List<Pokemon> dexList = test.GetPokemon;
        List<Pokemon> pokemon = dex.GetPokemon;

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


    //Find a reference to a Pokemon from its name.
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


    //Buttons M1-M4 denote each move of the Players Pokemon.
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


    //Updates values relating to each Pokemon and sends them through to the Turn System to Calculate how the battle progresses.
    void damageStuff(Move m)
    {
        Move foemove = foeMove();
        fMove = foemove;
        bool playerfirst = playerFirst();
        TextPanel.SetActive(true);
        int cHP = (int)pSlider.value;
        int fcHp = (int)slider.value;

        FindObjectOfType<TurnSystem>().doStuff(pActive, fActive, foemove, m, bText, playerfirst, cHP, fcHp);

    }


    //Find a reference to the players current Pokemon.
    Pokemon findActive()
    {
        List<Pokemon> pokemon = dex.GetPokemon;

        ActivePokemon = GameObject.FindGameObjectWithTag("Player");
        String pokemonName = ActivePokemon.transform.parent.tag;
        active = findMon(pokemonName, pokemon);
        pActive.P = active;
        return active;
    }


    //TODO: This is no longer needed, update code to reflect that.
    Move foeMove()
    {
        System.Random r = new System.Random();
        int x = r.Next(4);
        return foe.Moves[x];
    }


    //Checks which Pokemon is faster, if a speed tie occurs, randomise who is fastest for this round.
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


    //Update UI to reflect the fact the Pokemon has fainted.
    void pokemonFainted(Pokemon fainted)
    {
        List<Pokemon> pokedex = dex.GetPokemon;
        if (players)
        {
            deadPokemon.Add(active);
            AMod.text = "1";
            DMod.text = "1";
            SaMod.text = "1";
            SdMod.text = "1";
            GameObject playerPokemon = GameObject.FindGameObjectWithTag("Player");
            GameObject pP = playerPokemon.transform.parent.gameObject;
            int i = playable.IndexOf(fainted);
            int x = playerOrder.IndexOf(i);
            playerBalls[x].texture = balls[0];
            blockFainted();
            setSwitchText(fainted);
        }
        else
        {
            enemyPokemon = GameObject.FindGameObjectWithTag("Foe");
            GameObject gP = enemyPokemon.transform.parent.gameObject;
            int i = allFoePokemon.IndexOf(gP);
            int x = foeOrder.IndexOf(i);
            agentBalls[x].texture = balls[0];
            foeFaint(fainted);

        }

    }


    //Update the UI to show the newly sent out Pokemon.
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


    //Update dialogue.
    public void setSwitchText(Pokemon f)
    {

        pFainted = pFainted + 1;

        deadText.text = f.Name + " fainted...";
        if (pFainted == 6)
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
        else
        {
            switchPanel.SetActive(true);
            deadPanel.SetActive(true);
        }

    }


    //Agent chooses which Pokemon it should send out next. UI updates.
    public void foeFaint(Pokemon f)
    {
        sentences = new Queue<string>();
        fFainted = fFainted + 1;
        faintedAgent.Add(f);
        foeText.sentences[0] = f.Name + " fainted...";
        List<Pokemon> pokemon = dex.GetPokemon;

        if (fFainted < 6)
        {
            Agent agent = new Agent();
            Pokemon test = agent.chooseNext(faintedAgent, allFoePokemon, foeOrder, pokemon, active);
            next = findMon(test.Name, pokemon);
            foreach (GameObject g in allFoePokemon)
            {
                if (g.tag.Equals(next.Name))
                {
                    foeActiveObj = g;
                }
            }
            foeText.sentences[1] = "Foe sends out " + next.Name;
        }
        else
        {
            foeText.sentences[1] = "placeholder ";
        }
        foreach (string sentence in foeText.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foeChanged.SetActive(true);
        GameObject turnOff = enemyPokemon.transform.parent.gameObject;
        turnOff.SetActive(false);
        displayNext();

    }


    //Dialogue manager.
    public void displayNext()
    {
        if (sentences.Count == 1 && fFainted == 6 || sentences.Count == 1 && pFainted == 6)
        {
            gameOverPanel.SetActive(true);


            return;
        }
        if (sentences.Count == 0)
        {
            foeChanged.SetActive(false);
            return;
        }
        else if (sentences.Count == 1)
        {
            foeActiveObj.SetActive(true);
            wasSwitched = true;
            checkFainted = true;
        }
        string sentence = sentences.Dequeue();

        fText.text = sentence;

    }

    //Buttons U2-U6 handle the player switching Pokemon.
    public void onClickU2()
    {
        bool setActive = true;
        foreach (Pokemon p in deadPokemon)
        {
            if (p.Name == u2.text)
            {
                setActive = false;
                break;
            }
        }
        if (setActive)
        {
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

        s = s.ToUpper();

        foreach (GameObject g in allMyPokemon)
        {

            if (g.name.ToUpper().Contains(s))
            {
                g.SetActive(true);
                return;
            }
        }
    }


    //Gray out images of fainted Pokemon.
    void blockFainted()
    {
        foreach (Pokemon p in deadPokemon)
        {
            if (p.Name == u1.text)
            {
                b1.GetComponent<RawImage>().color = Color.gray;

            }
            if (p.Name == u2.text)
            {
                b2.GetComponent<RawImage>().color = Color.gray;
            }
            if (p.Name == u3.text)
            {
                b3.GetComponent<RawImage>().color = Color.gray;
            }
            if (p.Name == u4.text)
            {
                b4.GetComponent<RawImage>().color = Color.gray;
            }
            if (p.Name == u5.text)
            {
                b5.GetComponent<RawImage>().color = Color.gray;
            }
            if (p.Name == u6.text)
            {
                b6.GetComponent<RawImage>().color = Color.gray;
            }
        }
    }


    public void onClickRestart()
    {

        Restart();

    }
}
