using System.Collections.Generic;

class MoveList
{
    private static MoveList _instance;
    private List<Move> _moves = new List<Move>();

    private static object syncLock = new object();
   
    protected MoveList()
    {
        _moves.Add(new Move(0,"Vine Whip", "Grass", 45,"Damage","P"));//PHYS
        _moves.Add(new Move(1,"Ember", "Fire", 40,"Damage", "S")); //Sp
        _moves.Add(new Move(2,"Tackle", "Normal", 40, "Damage", "P")); //PHYS
        _moves.Add(new Move(3,"Scratch", "Normal",40, "Damage", "P")); //Phys
        _moves.Add(new Move(4,"Headbutt", "Normal", 70, "Damage", "P")); //Phys
        _moves.Add(new Move(5,"Watergun", "Water", 40, "Damage", "S")); //Sp 
        _moves.Add(new Move(6,"Flamethrower","Fire",90, "Damage", "S")); //SP
        _moves.Add(new Move(7, "Bite", "Dark", 60, "Damage", "P")); //Phys
        _moves.Add(new Move(8, "Flame Wheel", "Fire", 60, "Damage", "P")); //Phys
        _moves.Add(new Move(9, "Aqua Jet", "Water", 40,"First", "P")); //Phys effect
        _moves.Add(new Move(10, "Swift", "Normal", 60, "Damage", "S")); //Sp
        _moves.Add(new Move(11, "Giga Drain", "Grass", 75,"Drain", "S")); //Sp effect
        _moves.Add(new Move(12,"Synthesis","null",0,"Heal","null")); //Restore health
        _moves.Add(new Move(13, "Aura Sphere", "Fighting", 80, "Damage", "S")); //Sp
        _moves.Add(new Move(14, "Brick Break", "Fighting", 75, "Damage", "P")); //Phys
        _moves.Add(new Move(15, "Dragon Pulse", "Dragon", 85, "Damage", "S")); //SP
        _moves.Add(new Move(16, "Metal Claw", "Steel", 50, "Damage", "P")); //Phys
        _moves.Add(new Move(17,"Spark","Electric",65, "Damage", "P")); //Phys
        _moves.Add(new Move(18, "Discharge", "Electric", 80, "Damage", "S")); //Special
        _moves.Add(new Move(19, "Sonic Boom", "Normal", 0, "Damage", "S")); //Special
        _moves.Add(new Move(20, "Bulldoze", "Ground", 60, "Damage", "P")); //Phys
        _moves.Add(new Move(21, "Stomp", "Normal", 65, "Damage", "P")); //Phys
        _moves.Add(new Move(22, "Rock Slide", "Rock", 75, "Damage", "P")); //Phys
        _moves.Add(new Move(23, "Dragon Claw", "Dragon", 80, "Damage", "P")); //Phys
        _moves.Add(new Move(24, "Dragon Breath", "Dragon", 60, "Damage", "S")); //Sp
        _moves.Add(new Move(25, "Earth Power", "Ground", 90, "Damage", "S")); //Sp
        _moves.Add(new Move(26, "Thunder Fang", "Electric", 65, "Damage", "P")); //Phys
        _moves.Add(new Move(27, "Shadow Ball", "Ghost", 80, "Damage", "S")); //Sp




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
        get { 
        return _moves;
    }}


}