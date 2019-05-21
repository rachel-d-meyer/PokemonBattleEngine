using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Agent : MonoBehaviour
{

    public List<Node> Nodes = new List<Node>();
    public ActivePokemon p;
    public ActivePokemon a;
    public double[] startpStats = new double[6];
    public double[] startaStats = new double[6];
    public statArray agentStats;
    public statArray playerStats;
    List<Node> testList = new List<Node>();
    List<Node> holderList = new List<Node>();
    List<Node> filledNodes = new List<Node>();
    List<Node> otherList = new List<Node>();
    List<Node> removedNodes = new List<Node>();


    public Move agent(ActivePokemon P, ActivePokemon A, double[] PStats, double[] AStats)
    {
        Nodes.Clear();
        p = P;
        a = A;
        PStats.CopyTo(startpStats, 0);
        AStats.CopyTo(startaStats, 0);

        agentStats.Stats = AStats;
        playerStats.Stats = PStats;

        //Add the inital Node.
        Nodes.Add(new Node(0, new Move(), new ActivePokemon(), new double[6], new double[6], new int(), new List<int>(), new int(), 0, -1000, 1000));

        //Find all children of this Node.
        expandNodes(0);


        holderList = updateHolderList();

        foreach (Node n in holderList)
        {
            if (n.Parent == 0 && n.ID != 0 && !(n.AttackerStats[0] == 0 || n.DefenderStats[0] == 0))
            {
                ExpandNodes2(n.ID);
            }
        }

        holderList = updateHolderList();
        foreach (Node n in holderList)
        {
            if (n.Depth == 2 && !(n.AttackerStats[0] == 0 || n.DefenderStats[0] == 0))
            {
                expandNodes(n.ID);
            }
        }

        holderList = updateHolderList();
        foreach (Node n in holderList)
        {
            if (n.Depth == 3 && !(n.AttackerStats[0] == 0 || n.DefenderStats[0] == 0))
            {
                ExpandNodes2(n.ID);
            }
        }

        holderList = updateHolderList();

        //if depth is 3, nodes children have no children. 
        int lowestDepth = 0;
        foreach (Node n in holderList)
        {
            if (isLeaf(n))
            {
                Node parent = FindParent(n.Parent);
                Node holderNode = parent;
                //fill parent a-b with child heuristic 
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
                    catch (Exception)
                    {
                        //Debug.Log("Nothing left");
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

        //Debug.Log("Priority: " + playerpriority + "/" + agentpriority);

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
                //Debug.Log(n.ID);

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
                Debug.Log("Name is null! Oh no!");
                try
                {
                    high = -1000;
                    foreach (Node n in Nodes)
                    {
                        //Debug.Log(n.ID);

                        if (highNode.Children.Contains(n.ID))
                        {
                            if (n.HighValue > high)
                            {
                                high = n.HighValue;
                                choice = n.M;
                            }
                        }
                    }
                    Debug.Log("Tried");
                }
                catch (Exception e)
                {
                    Debug.Log("Caught!");
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
                    if (n.Depth == (priorityNode.Depth + 1))
                    {
                        //Why did I put this in???
                    }
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
        double[] agentsStats = new double[5];
        double[] playersStats = new double[5];

        Node parentNode = FindParent(parent);
        //Debug.Log("Parent ID:" + parentNode.ID + " Parent HP:" + parentNode.AttackerStats[0] + "," + parentNode.DefenderStats[0]);
        if (parentNode.ID > 0)
        {
            if ((parentNode.Depth + 1) % 2 == 0)
            {

                playersStats = transferStats(parentNode.AttackerStats);
                agentsStats = transferStats(parentNode.DefenderStats);

            }
            else
            {
                if (parentNode.Pokemon.Player)
                {
                    playersStats = transferStats(parentNode.AttackerStats);
                    agentsStats = transferStats(parentNode.DefenderStats);

                }
                else
                {
                    agentsStats = transferStats(parentNode.AttackerStats);
                    playersStats = transferStats(parentNode.DefenderStats);

                }

            }
        }
        else
        {
            int x = 0;
            foreach (int i in startaStats)
            {
                if (x < 5)
                {
                    agentStats.Stats[x] = i;
                    agentsStats[x] = i;
                    x++;
                }
            }
            x = 0;
            foreach (int i in startpStats)
            {
                if (x < 5)
                {



                    playerStats.Stats[x] = i;
                    playersStats[x] = i;
                    x++;
                }
            }

        }


        List<double[]> result = DamageCalculator.calc(m, a, p, agentsStats, playersStats);
        agentsStats = result[0];
        playersStats = result[1];


        double agentHP = (agentsStats[0] / a.P.Stats[0].HP) * 100;
        double playerHP = (playersStats[0] / p.P.Stats[0].HP) * 100;
        int value = (int)(agentHP - playerHP);
        int ID = Nodes.Count;
        // Debug.Log("New Node:" + ID + " Move: " + m.Name + "Value:" + value + "HP: " + agentsStats[0] + "," + playersStats[0] +"Depth: "+(parentNode.Depth+1));
        Nodes.Add(new Node(ID, m, a, new double[] { agentsStats[0], agentsStats[1], agentsStats[2], agentsStats[3], agentsStats[4] }, new double[] { playersStats[0], playersStats[1], playersStats[2], playersStats[3], playersStats[4] }, parent, new List<int>(), value, (parentNode.Depth + 1), -1000, 1000));
        updateParent(parentNode, ID);
    }

    public void AddPlayerNode(int parent, Move m)
    {
        bool playerFaster = p.P.Stats[0].Spd > a.P.Stats[0].Spd;
        bool pPriority = playerPriority();
        bool aPriority = agentPriority();
        double[] playersStats = new double[5];
        double[] agentsStats = new double[5];
        Node parentNode = FindParent(parent);
        //  Debug.Log("Parent ID:" + parentNode.ID + " Parent HP:" + parentNode.AttackerStats[0] + "," + parentNode.DefenderStats[0]);
        if (parentNode.ID > 0)
        {
            if (parentNode.Depth + 1 % 2 == 0)
            {
                playersStats = transferStats(parentNode.DefenderStats);
                agentsStats = transferStats(parentNode.AttackerStats);
            }
            else
            {
                if (parentNode.Pokemon.Player)
                {
                    playersStats = transferStats(parentNode.AttackerStats);
                    agentsStats = transferStats(parentNode.DefenderStats);

                }
                else
                {
                    agentsStats = transferStats(parentNode.AttackerStats);
                    playersStats = transferStats(parentNode.DefenderStats);

                }

            }
        }
        else
        {
            int x = 0;
            foreach (int i in startaStats)
            {
                if (x < 5)
                {



                    int y = i;
                    agentsStats[x] = i;
                    x++;
                }
            }
            x = 0;
            foreach (int i in startpStats)
            {
                if (x < 5)
                {


                    int y = i;
                    playersStats[x] = i;
                    x++;
                }
            }
        }

        List<double[]> result = DamageCalculator.calc(m, p, a, playersStats, agentsStats);
        playersStats = result[0];
        agentsStats = result[1];

        double agentHP = (agentsStats[0] / a.P.Stats[0].HP) * 100;
        double playerHP = (playersStats[0] / p.P.Stats[0].HP) * 100;

        int value = (int)(agentHP - playerHP);

        int ID = Nodes.Count;
        // Debug.Log("New Node:" + ID + " Move: "+m.Name+ "Value:" + value + "HP: "+agentsStats[0]+","+playersStats[0] + "Depth: " + (parentNode.Depth + 1));

        Nodes.Add(new Node(ID, m, p, new double[] { playersStats[0], playersStats[1], playersStats[2], playersStats[3], playersStats[4] }, new double[] { agentsStats[0], agentsStats[1], agentsStats[2], agentsStats[3], agentsStats[4] }, parent, new List<int>(), value, (parentNode.Depth + 1), -1000, 1000));
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
        // Debug.Log("Entered");
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
        //   Debug.Log("Added values");

        try
        {
            int hValue = heuristicValue[0];
            int lValue = heuristicValue[0];
            foreach (int x in heuristicValue)
            {
                // Debug.Log(x);

                if (x > hValue)
                {
                    hValue = x;
                }

                if (x < lValue)
                {
                    lValue = x;
                }

            }
            // Debug.Log("Found Alpha & Beta");
            n.HighValue = hValue;
            n.LowValue = lValue;
            //Debug.Log("Alpha: " + n.HighValue + " Beta: " + n.LowValue);
        }
        catch (ArgumentOutOfRangeException w)
        {
            // Debug.Log(heuristicValue.Count);
        }
        return n;
    }

    public Node backFill(Node n)
    {
        // Debug.Log("Backfill");
        List<int> children = n.Children;

        List<int> LowerValue = new List<int>();
        List<int> HigherValue = new List<int>();
        List<Node> childNodes = new List<Node>();


        foreach (Node node in Nodes)
        {

            if (children.Contains(node.ID))
            {
                //Debug.Log("B"+node.LowValue);
                //Debug.Log("A"+node.HighValue);
                LowerValue.Add(node.LowValue);
                HigherValue.Add(node.HighValue);
                if (n.ID >= 1 && n.ID <= 5)
                {
                    //Debug.Log("Child High: " + node.LowValue+ " ID: "+node.ID);
                    // Debug.Log("Child Low: " + node.LowValue);
                }
            }
        }
        if (children.Count > 1)
        {


            int hValue = HigherValue[0];
            int lValue = LowerValue[0];
            foreach (int x in LowerValue)
            {
                //Debug.Log(x);


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

            //Debug.Log("Alpha: " + hValue + " Beta: " + lValue);
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

    public double[] transferStats(double[] from)
    {
        double[] to = new double[6];
        int count = 0;
        foreach (double d in from)
        {
            if (count < 5)
            {
                to[count] = d;
            }
            count++;
        }

        return to;
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
                else
                {
                    // Debug.Log(i + " is a leaf!");
                }
            }
        }


        return true;
    }

    public bool findChild(int ID)
    {
        foreach (Node n in Nodes)
        {
            //Debug.Log("ID: " + n.ID + " Children: " + n.Children.Count);
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
            // Debug.Log("ID: " + n.ID + " Alpha: " + n.HighValue + " Beta: " + n.LowValue+" Move: "+n.M.Name);
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
        // Debug.Log(fainted.Count);
        List<String> choices = new List<String>();

        foreach (int i in foeOrder)
        {
            choices.Add(allFoePokemon[i].tag);
            //   Debug.Log(allFoePokemon[i].tag);
        }
        List<Pokemon> mon = new List<Pokemon>();
        foreach (String s in choices)
        {
            Pokemon p = findMon(s, allMon);
            mon.Add(p);
            // Debug.Log(p.Name);
            if (fainted.Contains(p))
            {
                mon.Remove(p);
                //   Debug.Log("Removed: " + p.Name);
            }
        }

        List<int> values = new List<int>();

        int mod = 0;
        TypeCheck _type = new TypeCheck();
        foreach (Pokemon p in mon)
        {
            mod = 0;
            if (_type.isSuperEffective(p, player))
            {
                mod = 50;
            }
            else if (_type.isNotEffective(p, player))
            {
                mod = -50;
            }
            else if (_type.isImmune(p, player))
            {
                mod = -100;
            }
            Debug.Log(mod);
            int i = p.Stats[0].HP + p.Stats[0].Atk + p.Stats[0].Def + p.Stats[0].SpAtk + p.Stats[0].SpDef + p.Stats[0].Spd + mod;
            Debug.Log("Pokemon: "+p.Name+" "+i);
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

        // Debug.Log(index);
        return mon[index];
        Debug.Log(allFoePokemon.Count);
        Debug.Log(foeOrder.Count);
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
