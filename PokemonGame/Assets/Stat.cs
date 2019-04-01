using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Stat {
    public int HP, Atk, Def, SpAtk, SpDef, Spd;

    public Stat(int hp, int atk, int def,int spatk, int spdef, int spd)
    {
        HP = hp;
        Atk = atk;
        Def = def;
        SpAtk = spatk;
        SpDef = spdef;
        Spd = spd;
    }
	
}
