using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Node {

    public int ID;
    public Move M;
    public ActivePokemon Pokemon;
    public double[] AttackerStats;
    public double[] DefenderStats;
    public int Parent;
    public List<int> Children;
    public int Value;
    public int Depth;
    public int HighValue;
    public int LowValue;

    public Node(int id, Move m, ActivePokemon pokemon, double[] attackerStats,double[] defenderStats, int parent,List<int> children, int value, int depth,int highValue, int lowValue)
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
