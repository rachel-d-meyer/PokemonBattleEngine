using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets;

public class Agent : MonoBehaviour
{

    public List<Node> Nodes = new List<Node>();
    public ActivePokemon p;
    public ActivePokemon a;
    public Stat startpStats;
    public Stat startaStats;
    public Stat agentStats;
    public Stat playerStats;
    List<Node> testList = new List<Node>();
    List<Node> holderList = new List<Node>();
    List<Node> filledNodes = new List<Node>();
    List<Node> otherList = new List<Node>();
    List<Node> removedNodes = new List<Node>();


    public Move agent(ActivePokemon P, ActivePokemon A, Stat PStats, Stat AStats)
    {
        Nodes.Clear();
        p = P;
        a = A;
        startpStats.CopyFrom(PStats);
        startaStats.CopyFrom(AStats);

        agentStats = AStats;
        playerStats = PStats;

        //Add the inital Node.
        Nodes.Add(new Node(0, new Move(), new ActivePokemon(), null, null, new int(), new List<int>(), new int(), 0, -1000, 1000));

        //Find all children of this Node.
        expandNodes(0);


        holderList = updateHolderList();

        foreach (Node n in holderList)
        {
            if (n.Parent == 0 && n.ID != 0 && !(n.AttackerStats.hp == 0 || n.DefenderStats.hp == 0))
            {
                ExpandNodes2(n.ID);
            }
        }

        holderList = updateHolderList();
        foreach (Node n in holderList)
        {
            if (n.Depth == 2 && !(n.AttackerStats.hp == 0 || n.DefenderStats.hp == 0))
            {
                expandNodes(n.ID);
            }
        }

        holderList = updateHolderList();
        foreach (Node n in holderList)
        {
            if (n.Depth == 3 && !(n.AttackerStats.hp == 0 || n.DefenderStats.hp == 0))
            {
                ExpandNodes2(n.ID);
            }
        }

        holderList = updateHolderList();

       //Find leaf nodes and fill parent A-B values from this.
        int lowestDepth = 0;
        foreach (Node n in holderList)
        {
            if (isLeaf(n))
            {
                Node parent = FindParent(n.Parent);
                Node holderNode = parent;
                int high = parent.HighValue;
                int low = parent.LowValue;
                Node newNode = n;
                newNode.HighValue = n.Value;
                newNode.LowValue = n.Value;
                Nodes[n.ID] = newNode;
                if (n.Value < low)
                {
                    low = n.Value;
                    parent.LowValue = low;
                    Nodes[parent.ID] = parent;
                }
                if (n.Value > high)
                {
                    high = n.Value;
                    parent.HighValue = high;
                    Nodes[parent.ID] = parent;
                }
                if (otherList.Contains(holderNode))
                {
                    otherList.Remove(holderNode);
                }
                otherList.Add(parent);

            }
            else
            {
                if (n.Depth > lowestDepth)
                {
                    lowestDepth = n.Depth;
                }
            }
        }

        //Update Nodes and add them to a list to backfill heuristic, node is removed from list when parent is filled.
        //When the list is empty all nodes will be backfilled with values.
        do
        {
            holderList = updateHolderList();

            foreach (Node n in holderList)
            {
                if (otherList.Contains(n))
                {
                    otherList.Remove(n);
                    try
                    {
                        Node parent = FindParent(n.Parent);
                        if (parent.ID == 0)
                        {
                            continue;
                        }
                        Node holderNode = parent;
                        int high = parent.HighValue;
                        int low = parent.LowValue;
                        Node newNode = n;
                        newNode.HighValue = n.Value;
                        newNode.LowValue = n.Value;
                        Nodes[n.ID] = newNode;

                        if (n.Value < low)
                        {
                            low = n.Value;
                            parent.LowValue = low;
                            Nodes[parent.ID] = parent;
                        }

                        if (n.Value > high)
                        {
                            high = n.Value;
                            parent.HighValue = high;
                            Nodes[parent.ID] = parent;
                        }

                        if (otherList.Contains(holderNode))
                        {
                            otherList.Remove(holderNode);
                        }
                        otherList.Add(parent);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e.Message);
                    }
                }
            }

        } while (otherList.Count > 0);

        Move bestMove = findBestMove();
        return bestMove;
    }


    public void expandNodes(int parent)
    {
        if (p.P.Stats[0].Spd > a.P.Stats[0].Spd)
        {

            foreach (Move m in p.P.Moves)
            {
                AddPlayerNode(parent, m);
            }
            foreach (Move m in a.P.Moves)
            {
                if (m.Info.Equals("First"))
                {
                    AddAgentNode(parent, m);
                }
            }
        }
        else
        {

            foreach (Move m in a.P.Moves)
            {
                AddAgentNode(parent, m);
            }
            foreach (Move m in p.P.Moves)
            {
                if (m.Info.Equals("First"))
                {
                    AddPlayerNode(parent, m);
                }
            }
        }
    }


    public void ExpandNodes2(int parent)
    {

        Node parentNode = FindParent(parent);
        bool isPlayer = parentNode.Pokemon.Player;
        bool playerFaster = p.P.Stats[0].Spd > a.P.Stats[0].Spd;

        if (isPlayer && playerFaster)
        {

            foreach (Move m in a.P.Moves)
            {
                if (!(m.Info.Equals("First")))
                {
                    AddAgentNode(parent, m);
                }
            }
        }
        else if (!(isPlayer) && !playerFaster)
        {

            foreach (Move m in p.P.Moves)
            {
                if (!m.Info.Equals("First"))
                {
                    AddPlayerNode(parent, m);
                }
            }
        }
        else if (!isPlayer && playerFaster)
        {

            foreach (Move m in a.P.Moves)
            {
                if (!m.Info.Equals("First"))
                {
                    AddAgentNode(parent, m);
                }
            }
        }
        else if (isPlayer && !playerFaster)
        {
            foreach (Move m in a.P.Moves)
            {
                AddAgentNode(parent, m);
            }
        }
        else
        {

            foreach (Move m in p.P.Moves)
            {
                if (!m.Info.Equals("First"))
                {
                    AddPlayerNode(parent, m);
                }
            }
        }

    }


    public Move findBestMove()
    {
        Node node = new Node();
        bool playerFirst = a.P.Stats[0].Spd < p.P.Stats[0].Spd;
        bool playerpriority = playerPriority();
        bool agentpriority = agentPriority();


        int low = 1000;
        int high = -1000;
        Node highNode = new Node();
        if (playerFirst && !agentpriority)
        {
            foreach (Node n in Nodes)
            {
                if (n.Depth == 1)
                {
                    if (n.LowValue < low)
                    {
                        low = n.LowValue;
                        node = n;
                    }
                    if (n.HighValue > high)
                    {
                        high = n.HighValue;
                        highNode = n;
                    }
                }
            }

            Move choice = new Move();
            foreach (Node n in Nodes)
            {
                if (node.Children.Contains(n.ID))
                {
                    if (n.HighValue > high)
                    {
                        high = n.HighValue;
                        choice = n.M;
                    }
                }
            }

            if (choice.Name == null)
            {
                try
                {
                    high = -1000;
                    foreach (Node n in Nodes)
                    {
                        if (highNode.Children.Contains(n.ID))
                        {
                            if (n.HighValue > high)
                            {
                                high = n.HighValue;
                                choice = n.M;
                            }
                        }
                    }
                    
                }
                catch (Exception e)
                {
                    Debug.Log(e.Message);
                    choice = a.P.Moves[0];
                }

            }
            if (choice.Name == null)
            {
                choice = a.P.Moves[0];
            }

            return choice;
        }
        else if (!playerFirst && !playerpriority)
        {
            Move choice = new Move();
            foreach (Node n in Nodes)
            {
                if (n.Depth == 1)
                {
                    if (n.HighValue > high)
                    {
                        high = n.HighValue;
                        choice = n.M;
                    }
                }
            }

            return choice;
        }
        else if (playerFirst && agentpriority)
        {
            Move choice = new Move();

            foreach (Node n in Nodes)
            {
                if (n.Depth == 1)
                {
                    if (n.Pokemon.Player)
                    {
                        if (n.LowValue < low)
                        {
                            low = n.LowValue;
                            node = n;
                        }
                    }
                    else
                    {
                        high = n.HighValue;
                        choice = n.M;
                    }
                }
            }

            foreach (Node n in Nodes)
            {
                if (node.Children.Contains(n.ID))
                {
                    if (n.HighValue > high)
                    {

                        high = n.HighValue;
                        choice = n.M;
                    }
                }
            }
            return choice;
        }
        else if (!playerFirst && playerpriority)
        {
            
            Move choice = new Move();
            Node priorityNode = new Node();
            bool priorityBestChoice = true;
            foreach (Node n in Nodes)
            {
                if (n.Depth == 1)
                {
                    if (n.Pokemon.Player)
                    {
                        priorityNode = n;
                        low = n.LowValue; 
                    }
                }
            }

            foreach (Node n in Nodes)
            {
                if (n.Depth == 2)
                {
                    if (n.Pokemon.Player)
                    {
                        if (n.LowValue < low)
                        {
                            priorityBestChoice = false;
                        }
                    }
                }
            }

            if (priorityBestChoice)
            {
               
                foreach (Node n in Nodes)
                {
                    if (priorityNode.Children.Contains(n.ID))
                    {
                        if (n.HighValue > high)
                        {
                            high = n.HighValue;
                            choice = n.M;  
                        }
                    }
                }

                if (choice.Name == null)
                {
                    choice = a.P.Moves[0];
                }
            }
            else
            {
                foreach (Node n in Nodes)
                {
                    if (n.Depth == 1)
                    {
                        if (!n.Pokemon.Player)
                        {
                            if (n.HighValue > high)
                            {

                                high = n.HighValue;
                                choice = n.M;
                            }
                        }
                    }
                }
            }
            return choice;
        }

        return a.P.Moves[0];
    }


    public void AddAgentNode(int parent, Move m)
    {
        bool playerFaster = p.P.Stats[0].Spd > a.P.Stats[0].Spd;
        bool pPriority = playerPriority();
        bool aPriority = agentPriority();
        Stat agentsStats = null;
        Stat playersStats = null;

        Node parentNode = FindParent(parent);
        if (parentNode.ID > 0)
        {
            if ((parentNode.Depth + 1) % 2 == 0)
            {
                playersStats = parentNode.AttackerStats.Copy();
                agentsStats = parentNode.DefenderStats.Copy();
            }
            else
            {
                if (parentNode.Pokemon.Player)
                {
                    playersStats = parentNode.AttackerStats.Copy();
                    agentsStats = parentNode.DefenderStats.Copy();
                }
                else
                {
                    agentsStats = parentNode.AttackerStats.Copy();
                    playersStats = parentNode.DefenderStats.Copy();
                }

            }
        }
        else
        {
            agentStats.CopyFrom(startaStats);
            agentsStats.CopyFrom(startaStats);
            playerStats.CopyFrom(startpStats);
            playersStats.CopyFrom(startpStats);
        }

        List<Stat> result = DamageCalculator.calc(m, a, p, agentsStats, playersStats);
        agentsStats = result[0];
        playersStats = result[1];

        double agentHP = (agentsStats.hp / a.P.Stats[0].HP) * 100;
        double playerHP = (playersStats.hp / p.P.Stats[0].HP) * 100;
        int value = (int)(agentHP - playerHP);
        int ID = Nodes.Count;
        Nodes.Add(new Node(ID, m, a, agentsStats.Copy(), playerStats.Copy(), parent, new List<int>(), value, (parentNode.Depth + 1), -1000, 1000));
        updateParent(parentNode, ID);
    }


    public void AddPlayerNode(int parent, Move m)
    {
        bool playerFaster = p.P.Stats[0].Spd > a.P.Stats[0].Spd;
        bool pPriority = playerPriority();
        bool aPriority = agentPriority();
        Stat playersStats = null;
        Stat agentsStats = null;
        Node parentNode = FindParent(parent);

        if (parentNode.ID > 0)
        {
            if (parentNode.Depth + 1 % 2 == 0)
            {
                playersStats = parentNode.DefenderStats.Copy();
                agentsStats = parentNode.AttackerStats.Copy();
            }
            else
            {
                if (parentNode.Pokemon.Player)
                {
                    playersStats = parentNode.AttackerStats.Copy();
                    agentsStats = parentNode.DefenderStats.Copy();
                }
                else
                {
                    agentsStats = parentNode.AttackerStats.Copy();
                    playersStats = parentNode.DefenderStats.Copy();
                }

            }
        }
        else
        {
            agentsStats.CopyFrom(startaStats);
            playersStats.CopyFrom(startpStats);
        }

        List<Stat> result = DamageCalculator.calc(m, p, a, playersStats, agentsStats);
        playersStats = result[0];
        agentsStats = result[1];

        double agentHP = (agentsStats.hp / a.P.Stats[0].HP) * 100;
        double playerHP = (playersStats.hp / p.P.Stats[0].HP) * 100;

        int value = (int)(agentHP - playerHP);

        int ID = Nodes.Count;

        Nodes.Add(new Node(ID, m, p, playersStats.Copy(), agentsStats.Copy(), parent, new List<int>(), value, (parentNode.Depth + 1), -1000, 1000));
        updateParent(parentNode, ID);
    }


    public Node FindParent(int parent)
    {
        foreach (Node n in Nodes)
        {
            if (n.ID == parent)
            {
                return n;
            }
        }

        return new Node();
    }


    public void updateParent(Node parent, int child)
    {
        parent.Children.Add(child);
    }


    public Node backFillHeuristic(Node n)
    {
        List<int> children = n.Children;
        List<int> heuristicValue = new List<int>();
        List<Node> childNodes = new List<Node>();
        foreach (Node node in Nodes)
        {
            if (children.Contains(node.ID))
            {
                heuristicValue.Add(node.Value);
            }
        }

        try
        {
            int hValue = heuristicValue[0];
            int lValue = heuristicValue[0];
            foreach (int x in heuristicValue)
            {

                if (x > hValue)
                {
                    hValue = x;
                }

                if (x < lValue)
                {
                    lValue = x;
                }

            }
            
            n.HighValue = hValue;
            n.LowValue = lValue;
           
        }
        catch (ArgumentOutOfRangeException w)
        {
            Debug.Log("Error");
        }
        return n;
    }


    public Node backFill(Node n)
    {
        List<int> children = n.Children;

        List<int> LowerValue = new List<int>();
        List<int> HigherValue = new List<int>();
        List<Node> childNodes = new List<Node>();


        foreach (Node node in Nodes)
        {

            if (children.Contains(node.ID))
            {
                
                LowerValue.Add(node.LowValue);
                HigherValue.Add(node.HighValue);
                
            }
        }
        if (children.Count > 1)
        {


            int hValue = HigherValue[0];
            int lValue = LowerValue[0];
            foreach (int x in LowerValue)
            {
                if (x < lValue)
                {
                    lValue = x;
                }
            }
            foreach (int x in HigherValue)
            {
                if (x > hValue)
                {
                    hValue = x;
                }
            }
            n.HighValue = hValue;
            n.LowValue = lValue;
        }
        return n;
    }


    public List<Node> updateHolderList()
    {
        holderList.Clear();
        foreach (Node n in Nodes)
        {
            holderList.Add(n);
        }

        return holderList;
    }


    public bool playerPriority()
    {
        foreach (Move m in p.P.Moves)
        {
            if (m.Info.Equals("First"))
            {
                return true;

            }
        }
        return false;
    }


    public bool agentPriority()
    {
        foreach (Move m in a.P.Moves)
        {
            if (m.Info.Equals("First"))
            {
                return true;
            }
        }
        return false;
    }

    public bool isChildLeaf(Node n)
    {
        List<int> children = n.Children;
        if (children.Count > 0)
        {
            foreach (int i in children)
            {
                bool x = findChild(i);
                if (!x)
                {
                    return false;
                }
              
            }
        }
        return true;
    }


    public bool findChild(int ID)
    {
        foreach (Node n in Nodes)
        {
            if (n.ID == ID && n.Children.Count <= 0)
            {
                return true;
            }
        }
        return false;
    }

    public void printNodes()
    {
        foreach (Node n in Nodes)
        {
            if (n.Children.Count > 0) { }
           
        }
    }

    public bool isLeaf(Node n)
    {
        if (n.Children.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public List<Node> findChildren(List<int> child)
    {
        List<Node> children = new List<Node>();
        foreach (int i in child)
        {
            children.Add(Nodes[i]);
        }
        return children;
    }



    public Pokemon chooseNext(List<Pokemon> fainted, List<GameObject> allFoePokemon, List<int> foeOrder, List<Pokemon> allMon, Pokemon player)
    {
     
        List<String> choices = new List<String>();

        foreach (int i in foeOrder)
        {
            choices.Add(allFoePokemon[i].tag);
          
        }
        List<Pokemon> mon = new List<Pokemon>();
        foreach (String s in choices)
        {
            Pokemon p = findMon(s, allMon);
            mon.Add(p);
          
            if (fainted.Contains(p))
            {
                mon.Remove(p);
           
            }
        }

        List<int> values = new List<int>();

        int mod = 0;
        TypeCheck _type = new TypeCheck();
        foreach (Pokemon p in mon)
        {
            mod = 0;
            if (_type.isSuperEffective(p.Type, player))
            {
                mod = 100;
            }
            else if (_type.isNotEffective(p.Type, player))
            {
                mod = -100;
            }
            else if (_type.isImmune(p.Type, player))
            {
                mod = -200;
            }
      
            int i = p.Stats[0].HP + p.Stats[0].Atk + p.Stats[0].Def + p.Stats[0].SpAtk + p.Stats[0].SpDef + p.Stats[0].Spd + mod;
         
            values.Add(i);
        }
        int index = 0;
        int highValue = values[0];
        foreach (int v in values)
        {
            if (v > highValue)
            {
                highValue = v;
                index = values.IndexOf(v);
            }
        }
        return mon[index];
    }


    public Pokemon findMon(String s, List<Pokemon> allMon)
    {


        foreach (Pokemon p in allMon)
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

   
}
