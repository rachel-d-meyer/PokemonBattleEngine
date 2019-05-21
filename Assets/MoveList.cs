using System.Collections.Generic;

class MoveList
{
    private static MoveList _instance;
    private List<Move> _moves = new List<Move>();

    private static object syncLock = new object();

    protected MoveList()
    {
        _moves.Add(new Move(0, "Vine Whip", "Grass", 45, "Damage", "P"));
        _moves.Add(new Move(1, "Ember", "Fire", 40, "Damage", "S")); 
        _moves.Add(new Move(2, "Tackle", "Normal", 40, "Damage", "P")); 
        _moves.Add(new Move(3, "Scratch", "Normal", 40, "Damage", "P")); 
        _moves.Add(new Move(4, "Headbutt", "Normal", 70, "Damage", "P")); 
        _moves.Add(new Move(5, "Watergun", "Water", 40, "Damage", "S")); 
        _moves.Add(new Move(6, "Flamethrower", "Fire", 90, "Damage", "S")); 
        _moves.Add(new Move(7, "Bite", "Dark", 60, "Damage", "P")); 
        _moves.Add(new Move(8, "Flame Wheel", "Fire", 60, "Damage", "P")); 
        _moves.Add(new Move(9, "Aqua Jet", "Water", 40, "First", "P")); 
        _moves.Add(new Move(10, "Swift", "Normal", 60, "Damage", "S")); 
        _moves.Add(new Move(11, "Giga Drain", "Grass", 75, "Drain", "S")); 
        _moves.Add(new Move(12, "Synthesis", "Null", 0, "Heal", "null")); 
        _moves.Add(new Move(13, "Aura Sphere", "Fighting", 80, "Damage", "S")); 
        _moves.Add(new Move(14, "Brick Break", "Fighting", 75, "Damage", "P")); 
        _moves.Add(new Move(15, "Dragon Pulse", "Dragon", 85, "Damage", "S")); 
        _moves.Add(new Move(16, "Metal Claw", "Steel", 50, "Damage", "P")); 
        _moves.Add(new Move(17, "Spark", "Electric", 65, "Damage", "P")); 
        _moves.Add(new Move(18, "Discharge", "Electric", 80, "Damage", "S")); 
        _moves.Add(new Move(19, "Sonic Boom", "Normal", 0, "Damage", "S")); 
        _moves.Add(new Move(20, "Bulldoze", "Ground", 60, "Damage", "P")); 
        _moves.Add(new Move(21, "Stomp", "Normal", 65, "Damage", "P")); 
        _moves.Add(new Move(22, "Rock Slide", "Rock", 75, "Damage", "P")); 
        _moves.Add(new Move(23, "Dragon Claw", "Dragon", 80, "Damage", "P")); 
        _moves.Add(new Move(24, "Dragon Breath", "Dragon", 60, "Damage", "S")); 
        _moves.Add(new Move(25, "Earth Power", "Ground", 90, "Damage", "S")); 
        _moves.Add(new Move(26, "Thunder Fang", "Electric", 65, "Damage", "P")); 
        _moves.Add(new Move(27, "Shadow Ball", "Ghost", 80, "Damage", "S")); 
        _moves.Add(new Move(28, "Wing Attack", "Flying", 60, "Damage", "P"));
        _moves.Add(new Move(29, "Fire Fang", "Fire", 65, "Damage", "P"));
        _moves.Add(new Move(30, "Power-Up Punch", "Fighting", 40, "AUp", "P"));
        _moves.Add(new Move(31, "Bullet Punch", "Steel", 40, "First", "P"));
        _moves.Add(new Move(32, "Drain Punch", "Fighting", 75, "Drain", "P"));
        _moves.Add(new Move(33, "Thunder Punch", "Electric", 75, "Damage", "P"));
        _moves.Add(new Move(34, "Close Combat", "Fighting", 120, "DDownSpDDown", "P"));
        _moves.Add(new Move(35, "Bulk Up", "Null", 0, "AUpDUp", "null"));
        _moves.Add(new Move(36, "Rolling Kick", "Fighting", 60, "Damage", "P"));
        _moves.Add(new Move(37, "Freeze Dry", "Ice", 70, "IW", "S"));
        _moves.Add(new Move(38, "Ice Beam", "Ice", 90, "Damage", "S"));
        _moves.Add(new Move(39, "Ice Shard", "Ice", 40, "First", "P"));
        _moves.Add(new Move(40, "Hidden Power (Fire)", "Fire", 60, "Damage", "S"));
        _moves.Add(new Move(41, "Cross Chop", "Fighting", 100, "Damage", "P"));
        _moves.Add(new Move(42, "Poison Jab", "Poison", 80, "Damage", "P"));
        _moves.Add(new Move(43, "Splash", "Null", 0, "Nothing", "null"));
        _moves.Add(new Move(44, "Flail", "Normal", 20, "Damage", "P"));
        _moves.Add(new Move(45, "Celebrate", "Null", 0, "Celebrate", "null"));
        _moves.Add(new Move(46, "Psychic", "Psychic", 90, "Damage", "S"));
        _moves.Add(new Move(47, "Calm Mind", "Null", 0, "SpAUpSpDUp", "null"));
        _moves.Add(new Move(48, "Magical Leaf", "Grass", 60, "Damage", "S"));
        _moves.Add(new Move(49, "Light Screen", "Null", 0, "SPH5", "null"));
        _moves.Add(new Move(50, "Nasty Ploty", "Null", 0, "SpAUp2", "null"));
        _moves.Add(new Move(51, "Hyper Voice", "Normal", 90, "Damage", "S"));
        _moves.Add(new Move(52, "Hidden Power (Ghost)", "Ghost", 60, "Damage", "S"));
        _moves.Add(new Move(53, "Slash", "Normal", 70, "Damage", "P"));
        _moves.Add(new Move(54, "Waterfall", "Water", 80, "Damage", "P"));
        _moves.Add(new Move(55, "Recover", "Null", 0, "Heal", "Null"));
        _moves.Add(new Move(56, "Signal Beam", "Bug", 75, "Damage", "S"));
        _moves.Add(new Move(57, "Ice Punch", "Ice", 75, "Damage", "P"));
        _moves.Add(new Move(58, "Sludge Wave", "Poison", 95, "Damage", "S"));
        _moves.Add(new Move(59, "Bubble Beam", "Water", 65, "Damage", "S"));
        _moves.Add(new Move(60, "Sludge Bomb", "Poison", 90, "Damage", "S"));
        _moves.Add(new Move(61, "Energy Ball", "Grass", 90, "Damage", "S"));
        _moves.Add(new Move(62, "Dark Pulse", "Dark", 80, "Damage", "S"));





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