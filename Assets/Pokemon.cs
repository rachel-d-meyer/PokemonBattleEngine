using System;
using System.Collections.Generic;

public struct Pokemon
{
    public string Name, Type;
    public List<Move> Moves;
    public List<BaseStat> Stats;
    public bool Playable;

	public Pokemon(string name, string type,List<Move> moves, List<BaseStat> stats,bool playable)
	{
        Name = name;
        Type = type;
        Moves = moves;
        Stats = stats;
        Playable = playable;

	}
}
