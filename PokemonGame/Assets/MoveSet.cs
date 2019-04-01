using System;
using System.Collections.Generic;

class MoveSet
{
    private static MoveSet _instance;
    private Dictionary<String, List<Move>> _moveset = new Dictionary<string, List<Move>>();
    private static object syncLock = new object();

   enum move
    {
        Vine_Whip,Ember,Tackle,Scratch,Headbutt,Watergun,Flamethrower,Bite,Flame_Wheel,Aqua_Jet,Swift,Giga_Drain,Synthesis,Aura_Sphere,Brick_Break,Dragon_Pulse,Metal_Claw,Spark,Discharge,Sonic_Boom,Bulldoze,Stomp,Rock_Slide,Dragon_Claw,Dragon_Breath,Earth_Power,Thunder_Fang,Shadow_Ball
    }

    protected MoveSet()
    {
        MoveList moveList = MoveList.GetMoveList();
        List<Move> moves = moveList.Moves;
        List<Move> bulbasaur = new List<Move>();
        List<Move> charmander = new List<Move>();
        List<Move> marshtomp = new List<Move>();
        List<Move> arcanine = new List<Move>();
        List<Move> buizel = new List<Move>();
        List<Move> turtwig = new List<Move>();
        List<Move> lucario = new List<Move>();
        List<Move> squirtle = new List<Move>();
        List<Move> electrode = new List<Move>();
        List<Move> rhyperior = new List<Move>();
        List<Move> flygon = new List<Move>();
        List<Move> jolteon = new List<Move>();

        foreach (Move m in moves)
        {
            if(m.ID == (int)move.Vine_Whip)
            {
                bulbasaur.Add(m);
            }
            if(m.ID == (int)move.Ember)
            {
                charmander.Add(m);
            }
            if(m.ID == (int)move.Tackle)
            {
                bulbasaur.Add(m);
                charmander.Add(m);
                marshtomp.Add(m);
                arcanine.Add(m);
                buizel.Add(m);
                squirtle.Add(m);
                electrode.Add(m);
            }
            if(m.ID == (int)move.Scratch)
            {
                bulbasaur.Add(m);
                charmander.Add(m);
                marshtomp.Add(m);
            }
            if(m.ID == (int)move.Headbutt)
            {
                bulbasaur.Add(m);
                charmander.Add(m);
                marshtomp.Add(m);
                turtwig.Add(m);
                squirtle.Add(m);
            }
            if(m.ID == (int)move.Watergun)
            {
                marshtomp.Add(m);
                buizel.Add(m);
                squirtle.Add(m);
            }
            if(m.ID == (int)move.Flamethrower){
                arcanine.Add(m);
            }
            if(m.ID == (int)move.Bite)
            {
                arcanine.Add(m);
                turtwig.Add(m);
                squirtle.Add(m);
            }
            if(m.ID == (int)move.Flame_Wheel)
            {
                arcanine.Add(m);
            }
            if(m.ID == (int)move.Aqua_Jet)
            {
                buizel.Add(m);
            }
            if(m.ID == (int)move.Swift)
            {
                buizel.Add(m);
                flygon.Add(m);
                jolteon.Add(m);
            }
            if(m.ID == (int)move.Giga_Drain)
            {
                turtwig.Add(m);
            }
            if(m.ID == (int)move.Synthesis)
            {
                turtwig.Add(m);
            }
            if(m.ID == (int)move.Aura_Sphere)
            {
                lucario.Add(m);
            }
            if(m.ID == (int)move.Brick_Break)
            {
                lucario.Add(m);
                rhyperior.Add(m);
            }
            if(m.ID == (int)move.Dragon_Pulse)
            {
                lucario.Add(m);
            }
            if(m.ID == (int)move.Metal_Claw)
            {
                lucario.Add(m);
            }
            if(m.ID == (int)move.Spark)
            {
                electrode.Add(m);
            }
            if(m.ID == (int)move.Discharge)
            {
                electrode.Add(m);
                jolteon.Add(m);
            }
            if(m.ID == (int)move.Sonic_Boom)
            {
                electrode.Add(m);
            }
            if(m.ID == (int)move.Bulldoze)
            {
                rhyperior.Add(m);
            }
            if(m.ID == (int)move.Stomp)
            {
                rhyperior.Add(m);
            }
            if(m.ID == (int)move.Rock_Slide)
            {
                rhyperior.Add(m);
            }
            if(m.ID == (int)move.Dragon_Claw)
            {
                flygon.Add(m);
            }
            if(m.ID == (int)move.Dragon_Breath)
            {
                flygon.Add(m);
            }
            if(m.ID == (int)move.Earth_Power)
            {
                flygon.Add(m);
            }
            if(m.ID == (int)move.Thunder_Fang)
            {
                jolteon.Add(m);
            }
            if(m.ID == (int)move.Shadow_Ball)
            {
                jolteon.Add(m);
            }


        }

        _moveset.Add("Bulbasaur", bulbasaur);
        _moveset.Add("Charmander", charmander);
        _moveset.Add("Marshtomp", marshtomp);
        _moveset.Add("Arcanine", arcanine);
        _moveset.Add("Buizel", buizel);
        _moveset.Add("Turtwig", turtwig);
        _moveset.Add("Lucario", lucario);
        _moveset.Add("Squirtle", squirtle);
        _moveset.Add("Electrode", electrode);
        _moveset.Add("Rhyperior", rhyperior);
        _moveset.Add("Flygon", flygon);
        _moveset.Add("Jolteon", jolteon);
      

    }

    public static MoveSet GetMoveSet()
    {
        if(_instance == null)
        {
            lock (syncLock)
            {
                if(_instance == null)
                {
                    _instance = new MoveSet();
                }
            }
        }
        return _instance;
    }

    public Dictionary<String,List<Move>> GetMovePool
    {
        get
        {
            return _moveset;
        }
    }
}
