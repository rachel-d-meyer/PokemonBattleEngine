using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public struct Node {

    public int ID;
    public Move M;
    public ActivePokemon Pokemon;
    public Stat AttackerStats;
    public Stat DefenderStats;
    public int Parent;
    public List<int> Children;
    public int Value;
    public int Depth;
    public int HighValue;
    public int LowValue;

    public Node(int id, Move m, ActivePokemon pokemon, Stat attackerStats, Stat defenderStats, int parent,List<int> children, int value, int depth,int highValue, int lowValue)
    {
        ID = id;
        M = m;
        Pokemon = pokemon;
        AttackerStats = attackerStats;
        DefenderStats = defenderStats;
        Parent = parent;
        Children = children;
        Value = value;
        Depth = depth;
        HighValue = highValue;
        LowValue = lowValue;
    }

}
