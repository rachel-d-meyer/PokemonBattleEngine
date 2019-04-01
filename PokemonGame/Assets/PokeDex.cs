using System.Collections;
using System.Collections.Generic;


class PokeDex
{
    private static PokeDex _instance;
    private List<Pokemon> _pokemon = new List<Pokemon>();
    private static object syncLock = new object();

    protected PokeDex()
    {
        MoveSet moveSet = MoveSet.GetMoveSet();
        Dictionary<string, List<Move>> movesFor = moveSet.GetMovePool;

        Stats stats = Stats.GetStats();
        Dictionary<string, List<Stat>> statsFor = stats.GetStatList;

        _pokemon.Add(new Pokemon("Bulbasaur","Grass",movesFor["Bulbasaur"],statsFor["Bulbasaur"],true));
        _pokemon.Add(new Pokemon("Charmander", "Fire", movesFor["Charmander"], statsFor["Charmander"],false));
        _pokemon.Add(new Pokemon("Marshtomp", "Water", movesFor["Marshtomp"], statsFor["Marshtomp"],false));
        _pokemon.Add(new Pokemon("Arcanine", "Fire", movesFor["Arcanine"], statsFor["Arcanine"],false));
        _pokemon.Add(new Pokemon("Buizel", "Water", movesFor["Buizel"], statsFor["Buizel"],false));
        _pokemon.Add(new Pokemon("Turtwig", "Grass", movesFor["Turtwig"], statsFor["Turtwig"],true));
        _pokemon.Add(new Pokemon("Lucario", "Fighting", movesFor["Lucario"], statsFor["Lucario"],true));
        _pokemon.Add(new Pokemon("Squirtle", "Water", movesFor["Squirtle"], statsFor["Squirtle"],false));
        _pokemon.Add(new Pokemon("Electrode", "Electric", movesFor["Electrode"], statsFor["Electrode"],true));
        _pokemon.Add(new Pokemon("Rhyperior", "Rock", movesFor["Rhyperior"], statsFor["Rhyperior"],true));
        _pokemon.Add(new Pokemon("Flygon", "Dragon", movesFor["Flygon"], statsFor["Flygon"],false));
        _pokemon.Add(new Pokemon("Jolteon", "Electric", movesFor["Jolteon"], statsFor["Jolteon"],true));
    }

    public static PokeDex GetPokeDex()
    {
        if(_instance == null)
        {
            lock (syncLock)
            {
                if(_instance == null)
                {
                    _instance = new PokeDex();
                }
            }
        }
        return _instance;
    }

    public List<Pokemon> GetPokemon
    {
        get
        {
            return _pokemon;
        }
    }


}

