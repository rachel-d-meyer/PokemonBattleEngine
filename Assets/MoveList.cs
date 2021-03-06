﻿using System.Collections.Generic;

class MoveList
{
    private static MoveList _instance;
    private List<Move> _moves = new List<Move>();

    private static object syncLock = new object();

    protected MoveList()
    {
        _moves.Add(new Move(0, "Vine Whip", "Grass", 45, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(1, "Ember", "Fire", 40, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(2, "Tackle", "Normal", 40, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(3, "Scratch", "Normal", 40, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(4, "Headbutt", "Normal", 70, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(5, "Watergun", "Water", 40, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(6, "Flamethrower", "Fire", 90, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(7, "Bite", "Dark", 60, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(8, "Flame Wheel", "Fire", 60, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(9, "Aqua Jet", "Water", 40, Move.InfoType.FIRST, "P")); 
        _moves.Add(new Move(10, "Swift", "Normal", 60, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(11, "Giga Drain", "Grass", 75, Move.InfoType.DRAIN, "S")); 
        _moves.Add(new Move(12, "Synthesis", "Null", 0, Move.InfoType.HEAL, "null")); 
        _moves.Add(new Move(13, "Aura Sphere", "Fighting", 80, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(14, "Brick Break", "Fighting", 75, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(15, "Dragon Pulse", "Dragon", 85, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(16, "Metal Claw", "Steel", 50, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(17, "Spark", "Electric", 65, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(18, "Discharge", "Electric", 80, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(19, "Sonic Boom", "Normal", 0, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(20, "Bulldoze", "Ground", 60, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(21, "Stomp", "Normal", 65, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(22, "Rock Slide", "Rock", 75, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(23, "Dragon Claw", "Dragon", 80, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(24, "Dragon Breath", "Dragon", 60, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(25, "Earth Power", "Ground", 90, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(26, "Thunder Fang", "Electric", 65, Move.InfoType.DAMAGE, "P")); 
        _moves.Add(new Move(27, "Shadow Ball", "Ghost", 80, Move.InfoType.DAMAGE, "S")); 
        _moves.Add(new Move(28, "Wing Attack", "Flying", 60, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(29, "Fire Fang", "Fire", 65, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(30, "Power-Up Punch", "Fighting", 40, Move.InfoType.ATTACK_UP, "P"));
        _moves.Add(new Move(31, "Bullet Punch", "Steel", 40, Move.InfoType.FIRST, "P"));
        _moves.Add(new Move(32, "Drain Punch", "Fighting", 75, Move.InfoType.DRAIN, "P"));
        _moves.Add(new Move(33, "Thunder Punch", "Electric", 75, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(34, "Close Combat", "Fighting", 120, Move.InfoType.DEFENCE_DOWN_SPECIAL_DEFENCE_DOWN, "P"));
        _moves.Add(new Move(35, "Bulk Up", "Null", 0, Move.InfoType.ATTACK_UP_DEFENCE_UP, "null"));
        _moves.Add(new Move(36, "Rolling Kick", "Fighting", 60, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(37, "Freeze Dry", "Ice", 70, Move.InfoType.IGNORES_WATER, "S"));
        _moves.Add(new Move(38, "Ice Beam", "Ice", 90, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(39, "Ice Shard", "Ice", 40, Move.InfoType.FIRST, "P"));
        _moves.Add(new Move(40, "Hidden Power (Fire)", "Fire", 60, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(41, "Cross Chop", "Fighting", 100, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(42, "Poison Jab", "Poison", 80, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(43, "Splash", "Null", 0, Move.InfoType.NOTHING, "null"));
        _moves.Add(new Move(44, "Flail", "Normal", 20, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(45, "Celebrate", "Null", 0, Move.InfoType.CELEBRATE, "null"));
        _moves.Add(new Move(46, "Psychic", "Psychic", 90, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(47, "Calm Mind", "Null", 0, Move.InfoType.SPECIAL_ATTACK_UP_SPECIAL_DEFENCE_UP, "null"));
        _moves.Add(new Move(48, "Magical Leaf", "Grass", 60, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(50, "Nasty Ploty", "Null", 0, Move.InfoType.SPECIAL_ATTACK_UP_2, "null"));
        _moves.Add(new Move(51, "Hyper Voice", "Normal", 90, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(52, "Hidden Power (Ghost)", "Ghost", 60, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(53, "Slash", "Normal", 70, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(54, "Waterfall", "Water", 80, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(55, "Recover", "Null", 0, Move.InfoType.HEAL, "Null"));
        _moves.Add(new Move(56, "Signal Beam", "Bug", 75, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(57, "Ice Punch", "Ice", 75, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(58, "Sludge Wave", "Poison", 95, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(59, "Bubble Beam", "Water", 65, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(60, "Sludge Bomb", "Poison", 90, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(61, "Energy Ball", "Grass", 90, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(62, "Dark Pulse", "Dark", 80, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(63, "Crunch", "Dark", 80, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(64, "Fire Punch", "Fire", 75, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(65, "Icy Wind", "Ice", 55, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(66, "Focus Blast", "Fighting", 120, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(67, "Meteor Mash", "Steel", 90, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(68, "Foul Play", "Dark", 95, Move.InfoType.FOE, "P"));
        _moves.Add(new Move(69, "Bug Bite", "Bug", 60, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(70, "Swords Dance", "Null", 0, Move.InfoType.ATTACK_UP_2, "null"));
        _moves.Add(new Move(71, "Aerial Ace", "Flying", 60, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(72, "Struggle Bug", "Bug", 50, Move.InfoType.FOE_SPECIAL_ATTACK_DOWN, "S"));
        _moves.Add(new Move(73, "Dazzling Gleam", "Fairy", 80, Move.InfoType.DAMAGE, "S"));
        _moves.Add(new Move(74, "Defense Curl", "Null", 0, Move.InfoType.DEFENCE_UP, "null"));
        _moves.Add(new Move(75, "Soft Boiled", "Null", 0, Move.InfoType.HEAL, "null"));
        _moves.Add(new Move(76, "Seismic Toss", "Normal", 0, Move.InfoType.DAMAGE, "P"));
        _moves.Add(new Move(77, "Draining Kiss", "Fairy", 50, Move.InfoType.LEECH, "S"));
        _moves.Add(new Move(78, "Quick Attack", "Normal", 40, Move.InfoType.FIRST, "P"));
        _moves.Add(new Move(79, "Moonlight", "Null", 0, Move.InfoType.HEAL, "null"));

















    }

    public static MoveList GetMoveList()
    {
        if (_instance == null)
        {
            lock (syncLock)
            {
                if (_instance == null)
                {
                    _instance = new MoveList();
                }
            }
        }
        return _instance;
    }

    public List<Move> Moves
    {
        get
        {
            return _moves;
        }
    }


}