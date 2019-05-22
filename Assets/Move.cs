using System;

public struct Move
{
    public int ID;
    public string Name,Type;
    public int Power;
    public InfoType Info;
    public string Attack;

    public enum InfoType
    {
        DAMAGE,
        FIRST,
        DRAIN,
        HEAL,
        ATTACK_UP,
        DEFENCE_DOWN_SPECIAL_DEFENCE_DOWN,
        ATTACK_UP_DEFENCE_UP,
        NOTHING,
        CELEBRATE,
        IGNORES_WATER,
        SPECIAL_ATTACK_UP_SPECIAL_DEFENCE_UP,
        SPECIAL_ATTACK_UP_2,
        FOE,
        ATTACK_UP_2,
        FOE_SPECIAL_ATTACK_DOWN,
        DEFENCE_UP,
        LEECH
    }

	public Move(int id,string name, string type, int power, InfoType info,string attack)
	{
        ID = id;
        Name = name;
        Type = type;
        Power = power;
        Info = info;
        Attack = attack;
	}
}
