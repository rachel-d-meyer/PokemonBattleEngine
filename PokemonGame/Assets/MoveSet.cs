using System;
using System.Collections.Generic;

class MoveSet
{
    private static MoveSet _instance;
    private Dictionary<String, List<Move>> _moveset = new Dictionary<string, List<Move>>();
    private static object syncLock = new object();

   enum move
    {
        Vine_Whip,Ember,Tackle,Scratch,Headbutt,Watergun,Flamethrower,Bite,Flame_Wheel,Aqua_Jet,Swift,Giga_Drain,Synthesis,Aura_Sphere,Brick_Break,Dragon_Pulse,Metal_Claw,Spark,Discharge,Sonic_Boom,Bulldoze,Stomp,Rock_Slide,Dragon_Claw,Dragon_Breath,Earth_Power,Thunder_Fang,Shadow_Ball,Wing_Attack,Fire_Fang,Power_Up_Punch,Bullet_Punch,Drain_Punch,Thunder_Punch,Close_Combat,Bulk_Up,Rolling_Kick,Freeze_Dry,Ice_Beam,Ice_Shard,Hidden_Power_Fire,Cross_Chop,Poison_Jab,Splash,Flail,Celebrate,Psychic,Calm_Mind,Magical_Leaf,Light_Screen,Nasty_Plot,Hyper_Voice,Hidden_Power_Ghost,Slash,Waterfall,Recover,Signal_Beam,Ice_Punch,Sludge_Wave,Bubble_Beam,Sludge_Bomb,Energy_Ball,Dark_Pulse
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
        List<Move> blastoise = new List<Move>();
        List<Move> charizard = new List<Move>();
        List<Move> hitmonchan = new List<Move>();
        List<Move> hitmonlee = new List<Move>();
        List<Move> lapras = new List<Move>();
        List<Move> machop = new List<Move>();
        List<Move> magikarp = new List<Move>();
        List<Move> mewtwo = new List<Move>();
        List<Move> mrmime = new List<Move>();
        List<Move> persian = new List<Move>();
        List<Move> poliwrath = new List<Move>();
        List<Move> porygon = new List<Move>();
        List<Move> tentacruel = new List<Move>();
        List<Move> venusaur = new List<Move>();
        List<Move> vulpix = new List<Move>();
        
        

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
                magikarp.Add(m);
            }
            if(m.ID == (int)move.Scratch)
            {
                bulbasaur.Add(m);
                charmander.Add(m);
                marshtomp.Add(m);
                vulpix.Add(m);
                
            }
            if(m.ID == (int)move.Headbutt)
            {
                bulbasaur.Add(m);
                charmander.Add(m);
                marshtomp.Add(m);
                turtwig.Add(m);
                
            }
            if(m.ID == (int)move.Watergun)
            {
                marshtomp.Add(m);
                buizel.Add(m);
                squirtle.Add(m);
            }
            if(m.ID == (int)move.Flamethrower){
                arcanine.Add(m);
                charizard.Add(m);
                mewtwo.Add(m);
                vulpix.Add(m);
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
                venusaur.Add(m);
            }
            if(m.ID == (int)move.Synthesis)
            {
                turtwig.Add(m);
                venusaur.Add(m);
            }
            if(m.ID == (int)move.Aura_Sphere)
            {
                lucario.Add(m);
                blastoise.Add(m);
            }
            if(m.ID == (int)move.Brick_Break)
            {
                lucario.Add(m);
                rhyperior.Add(m);
                poliwrath.Add(m);
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
                hitmonlee.Add(m);
                machop.Add(m);
            }
            if(m.ID == (int)move.Dragon_Claw)
            {
                flygon.Add(m);
                charizard.Add(m);
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
            if(m.ID == (int)move.Wing_Attack)
            {
                charizard.Add(m);
            }
            if(m.ID == (int)move.Fire_Fang)
            {
                charizard.Add(m);
            }
            if(m.ID == (int)move.Power_Up_Punch)
            {
                hitmonchan.Add(m);
                poliwrath.Add(m);
            }
            if(m.ID == (int)move.Bullet_Punch)
            {
                hitmonchan.Add(m);
                machop.Add(m);
            }
            if(m.ID == (int)move.Drain_Punch)
            {
                hitmonchan.Add(m);
            }
            if(m.ID == (int)move.Thunder_Punch)
            {
                hitmonchan.Add(m);
            }
            if(m.ID == (int)move.Close_Combat)
            {
                hitmonlee.Add(m);
            }
            if(m.ID == (int)move.Rolling_Kick)
            {
                hitmonlee.Add(m);
            }
            if(m.ID == (int)move.Bulk_Up)
            {
                hitmonlee.Add(m);
                poliwrath.Add(m);
            }
            if(m.ID == (int)move.Freeze_Dry)
            {
                lapras.Add(m);
            }
            if(m.ID == (int)move.Ice_Beam)
            {
                lapras.Add(m);
                mewtwo.Add(m);
                porygon.Add(m);
                blastoise.Add(m);
            }
            if(m.ID == (int)move.Ice_Shard)
            {
                lapras.Add(m);
            }
            if(m.ID == (int)move.Hidden_Power_Fire)
            {
                lapras.Add(m);
                venusaur.Add(m);

            }
            if(m.ID == (int)move.Cross_Chop)
            {
                machop.Add(m);
            }
            if(m.ID == (int)move.Poison_Jab)
            {
                machop.Add(m);
                tentacruel.Add(m);
            }
            if(m.ID == (int)move.Splash)
            {
                magikarp.Add(m);
            }
            if(m.ID == (int)move.Flail)
            {
                magikarp.Add(m);
            }
            if(m.ID == (int)move.Celebrate)
            {
                magikarp.Add(m);
            }
            if(m.ID == (int)move.Psychic)
            {
                mewtwo.Add(m);
                mrmime.Add(m);
                porygon.Add(m);
            }
            if(m.ID == (int)move.Calm_Mind)
            {
                mewtwo.Add(m);
                mrmime.Add(m);
            }
            if(m.ID == (int)move.Magical_Leaf)
            {
                mrmime.Add(m);
            }
            if(m.ID == (int)move.Light_Screen)
            {
                mrmime.Add(m);
            }
            if(m.ID == (int)move.Nasty_Plot)
            {
                persian.Add(m);
            }
            if(m.ID == (int)move.Hyper_Voice)
            {
                persian.Add(m);
            }
            if(m.ID == (int)move.Hidden_Power_Ghost)
            {
                persian.Add(m);
            }
            if(m.ID == (int)move.Slash)
            {
                persian.Add(m);
            }
            if(m.ID == (int)move.Waterfall)
            {
                poliwrath.Add(m);
                tentacruel.Add(m);
                blastoise.Add(m);
            }
            if(m.ID == (int)move.Recover)
            {
                porygon.Add(m);
            }
            if(m.ID == (int)move.Signal_Beam)
            {
                porygon.Add(m);
            }
            if(m.ID == (int)move.Ice_Punch)
            {
                squirtle.Add(m);
            }
            if(m.ID == (int)move.Sludge_Wave)
            {
                tentacruel.Add(m);
            }
            if (m.ID == (int)move.Bubble_Beam)
            {
                tentacruel.Add(m);
            }
            if(m.ID == (int)move.Sludge_Bomb)
            {
                venusaur.Add(m);
            }
            if(m.ID == (int)move.Energy_Ball)
            {
                vulpix.Add(m);
            }
            if(m.ID == (int)move.Dark_Pulse)
            {
                vulpix.Add(m);
                blastoise.Add(m);
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
        _moveset.Add("Blastoise", blastoise);
        _moveset.Add("Charizard", charizard);
        _moveset.Add("Hitmonchan", hitmonchan);
        _moveset.Add("Hitmonlee", hitmonlee);
        _moveset.Add("Lapras", lapras);
        _moveset.Add("Machop", machop);
        _moveset.Add("Magikarp", magikarp);
        _moveset.Add("MrMime", mrmime);
        _moveset.Add("Mewtwo", mewtwo);
        _moveset.Add("Persian", persian);
        _moveset.Add("Poliwrath", poliwrath);
        _moveset.Add("Porygon", porygon);
        _moveset.Add("Tentacruel", tentacruel);
        _moveset.Add("Venusaur", venusaur);
        _moveset.Add("Vulpix", vulpix);

      

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
