using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BaseStats {
    private static BaseStats _instance;
    private Dictionary<string, List<BaseStat>> _stats = new Dictionary<string, List<BaseStat>>();
    private static object syncLock = new object();

    protected BaseStats()
    {
        //HP = (2*base)*LVL/100 + LVL+ 10
        //OTHER = (2*base)*LVL/100 + 5
        _stats.Add("Bulbasaur", new List<BaseStat> { new BaseStat (HPCalc(45), statCalc(49), statCalc(49), statCalc(65), statCalc(65), statCalc(45))});
        _stats.Add("Charmander", new List<BaseStat> {new BaseStat( HPCalc(39), statCalc(52),statCalc(43),statCalc(60),statCalc(50),statCalc(65))});
        _stats.Add("Marshtomp", new List<BaseStat> {new BaseStat ( HPCalc(70),statCalc(85), statCalc(70), statCalc(60), statCalc(70),statCalc(50))});
        _stats.Add("Arcanine", new List<BaseStat> { new BaseStat(HPCalc(90), statCalc(110), statCalc(80), statCalc(100), statCalc(80), statCalc(95)) });
        _stats.Add("Buizel", new List<BaseStat> { new BaseStat (HPCalc(55),statCalc(65), statCalc(35),statCalc(60),statCalc(30), statCalc(85))});
        _stats.Add("Turtwig", new List<BaseStat> { new BaseStat(HPCalc(55), statCalc(68), statCalc(64), statCalc(45), statCalc(55), statCalc(31)) });
        _stats.Add("Lucario", new List<BaseStat> { new BaseStat(HPCalc(70), statCalc(110), statCalc(70), statCalc(115), statCalc(70), statCalc(90)) });
        _stats.Add("Squirtle", new List<BaseStat> { new BaseStat(HPCalc(44), statCalc(48), statCalc(65), statCalc(50), statCalc(64), statCalc(43)) });
        _stats.Add("Electrode", new List<BaseStat> { new BaseStat(HPCalc(60), statCalc(50), statCalc(70), statCalc(80), statCalc(80), statCalc(140)) });
        _stats.Add("Rhyperior", new List<BaseStat> { new BaseStat(HPCalc(115), statCalc(140), statCalc(130), statCalc(55), statCalc(55), statCalc(40)) });
        _stats.Add("Flygon", new List<BaseStat> { new BaseStat(HPCalc(80), statCalc(100), statCalc(80), statCalc(80), statCalc(80), statCalc(100)) });
        _stats.Add("Jolteon", new List<BaseStat> { new BaseStat(HPCalc(65), statCalc(65), statCalc(60), statCalc(110), statCalc(95), statCalc(130)) });
        _stats.Add("Blastoise", new List<BaseStat> { new BaseStat(HPCalc(79), statCalc(83), statCalc(100), statCalc(85), statCalc(105), statCalc(78)) });
        _stats.Add("Charizard", new List<BaseStat> { new BaseStat(HPCalc(78), statCalc(84), statCalc(78), statCalc(109), statCalc(85), statCalc(100)) });
        _stats.Add("Hitmonchan", new List<BaseStat> { new BaseStat(HPCalc(50), statCalc(105), statCalc(79), statCalc(35), statCalc(110), statCalc(76)) });
        _stats.Add("Hitmonlee", new List<BaseStat> { new BaseStat(HPCalc(50), statCalc(120), statCalc(53), statCalc(35), statCalc(110), statCalc(87)) });
        _stats.Add("Lapras", new List<BaseStat> { new BaseStat(HPCalc(130), statCalc(85), statCalc(80), statCalc(85), statCalc(95), statCalc(60)) });
        _stats.Add("Machop", new List<BaseStat> { new BaseStat(HPCalc(70), statCalc(80), statCalc(50), statCalc(35), statCalc(35), statCalc(35)) });
        _stats.Add("Magikarp", new List<BaseStat> { new BaseStat(HPCalc(20), statCalc(10), statCalc(55), statCalc(15), statCalc(20), statCalc(80)) });
        _stats.Add("Mewtwo", new List<BaseStat> { new BaseStat(HPCalc(106), statCalc(110), statCalc(90), statCalc(154), statCalc(90), statCalc(130)) });
        _stats.Add("MrMime", new List<BaseStat> { new BaseStat(HPCalc(40), statCalc(45), statCalc(65), statCalc(100), statCalc(120), statCalc(90)) });
        _stats.Add("Persian", new List<BaseStat> { new BaseStat(HPCalc(65), statCalc(70), statCalc(60), statCalc(65), statCalc(65), statCalc(115)) });
        _stats.Add("Poliwrath", new List<BaseStat> { new BaseStat(HPCalc(90), statCalc(85), statCalc(95), statCalc(70), statCalc(90), statCalc(70)) });
        _stats.Add("Porygon", new List<BaseStat> { new BaseStat(HPCalc(65), statCalc(60), statCalc(70), statCalc(85), statCalc(75), statCalc(40)) });
        _stats.Add("Tentacruel", new List<BaseStat> { new BaseStat(HPCalc(80), statCalc(70), statCalc(65), statCalc(80), statCalc(120), statCalc(100)) });
        _stats.Add("Venusaur", new List<BaseStat> { new BaseStat(HPCalc(80), statCalc(82), statCalc(83), statCalc(100), statCalc(100), statCalc(80)) });
        _stats.Add("Vulpix", new List<BaseStat> { new BaseStat(HPCalc(38), statCalc(41), statCalc(40), statCalc(50), statCalc(65), statCalc(65)) });
        _stats.Add("Tyranitar", new List<BaseStat> { new BaseStat(HPCalc(100), statCalc(134), statCalc(110), statCalc(95), statCalc(100), statCalc(61)) });
        _stats.Add("Gengar", new List<BaseStat> { new BaseStat(HPCalc(60), statCalc(65), statCalc(60), statCalc(130), statCalc(75), statCalc(110)) });

    }


    public static BaseStats GetStats()
    {
        if(_instance == null)
        {
            lock (syncLock)
            {
                if(_instance == null)
                {
                    _instance = new BaseStats();
                }
            }
        }
        return _instance;
    }


    public Dictionary<string, List<BaseStat>> GetStatList
    {
        get
        {
            return _stats;
        }
    }


    public int statCalc(int x)
    {
        x = (2 * x) * 50 / 100 + 5;
        return x;
    }

    public int HPCalc(int x)
    {
        x = (2 * x) * 50 / 100 + 50 + 10;
        return x;
    }
}
