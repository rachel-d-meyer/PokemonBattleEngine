using System;

public struct Move
{
    public int ID;
    public string Name,Type;
    public int Power;
    public string Info;
    public string Attack;

	public Move(int id,string name, string type, int power,string info,string attack)
	{
        ID = id;
        Name = name;
        Type = type;
        Power = power;
        Info = info;
        Attack = attack;
	}
}
