using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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

        _pokemon.Add(new Pokemon("Bulbasaur", "Grass", movesFor["Bulbasaur"], statsFor["Bulbasaur"], true));
        _pokemon.Add(new Pokemon("Charmander", "Fire", movesFor["Charmander"], statsFor["Charmander"], false));
        _pokemon.Add(new Pokemon("Marshtomp", "Water", movesFor["Marshtomp"], statsFor["Marshtomp"], false));
        _pokemon.Add(new Pokemon("Arcanine", "Fire", movesFor["Arcanine"], statsFor["Arcanine"], false));
        _pokemon.Add(new Pokemon("Buizel", "Water", movesFor["Buizel"], statsFor["Buizel"], false));
        _pokemon.Add(new Pokemon("Turtwig", "Grass", movesFor["Turtwig"], statsFor["Turtwig"], true));
        _pokemon.Add(new Pokemon("Lucario", "Fighting", movesFor["Lucario"], statsFor["Lucario"], true));
        _pokemon.Add(new Pokemon("Squirtle", "Water", movesFor["Squirtle"], statsFor["Squirtle"], false));
        _pokemon.Add(new Pokemon("Electrode", "Electric", movesFor["Electrode"], statsFor["Electrode"], true));
        _pokemon.Add(new Pokemon("Rhyperior", "Rock", movesFor["Rhyperior"], statsFor["Rhyperior"], true));
        _pokemon.Add(new Pokemon("Flygon", "Dragon", movesFor["Flygon"], statsFor["Flygon"], false));
        _pokemon.Add(new Pokemon("Jolteon", "Electric", movesFor["Jolteon"], statsFor["Jolteon"], true));
        _pokemon.Add(new Pokemon("Blastoise", "Water", movesFor["Blastoise"], statsFor["Blastoise"], true));
        _pokemon.Add(new Pokemon("Charizard", "Fire", movesFor["Charizard"], statsFor["Charizard"], true));
        _pokemon.Add(new Pokemon("Hitmonchan", "Fighting", movesFor["Hitmonchan"], statsFor["Hitmonchan"], true));
        _pokemon.Add(new Pokemon("Hitmonlee", "Fighting", movesFor["Hitmonlee"], statsFor["Hitmonlee"], true));
        _pokemon.Add(new Pokemon("Lapras", "Ice", movesFor["Lapras"], statsFor["Lapras"], true));
        _pokemon.Add(new Pokemon("Machop", "Fighting", movesFor["Machop"], statsFor["Machop"], true));
        _pokemon.Add(new Pokemon("Magikarp", "Water", movesFor["Magikarp"], statsFor["Magikarp"], true));
        _pokemon.Add(new Pokemon("MrMime", "Psychic", movesFor["MrMime"], statsFor["MrMime"], true));
        _pokemon.Add(new Pokemon("Mewtwo", "Psychic", movesFor["Mewtwo"], statsFor["Mewtwo"], true));
        _pokemon.Add(new Pokemon("Persian", "Normal", movesFor["Persian"], statsFor["Persian"], true));
        _pokemon.Add(new Pokemon("Poliwrath", "Water", movesFor["Poliwrath"], statsFor["Poliwrath"], true));
        _pokemon.Add(new Pokemon("Porygon", "Normal", movesFor["Porygon"], statsFor["Porygon"], true));
        _pokemon.Add(new Pokemon("Tentacruel", "Water", movesFor["Tentacruel"], statsFor["Tentacruel"], true));
        _pokemon.Add(new Pokemon("Venusaur", "Grass", movesFor["Venusaur"], statsFor["Venusaur"], true));
        _pokemon.Add(new Pokemon("Vulpix", "Fire", movesFor["Vulpix"], statsFor["Vulpix"], true));
        _pokemon.Add(new Pokemon("Tyranitar", "Rock", movesFor["Tyranitar"], statsFor["Tyranitar"], true));
        _pokemon.Add(new Pokemon("Gengar", "Ghost", movesFor["Gengar"], statsFor["Gengar"], true));
        _pokemon.Add(new Pokemon("Slaking", "Normal", movesFor["Slaking"], statsFor["Slaking"], true));
        _pokemon.Add(new Pokemon("Metagross", "Steel", movesFor["Metagross"], statsFor["Metagross"], true));
        _pokemon.Add(new Pokemon("Archeops", "Flying", movesFor["Archeops"], statsFor["Archeops"], true));
        _pokemon.Add(new Pokemon("Blissey", "Normal", movesFor["Blissey"], statsFor["Blissey"], true));
        _pokemon.Add(new Pokemon("Umbreon", "Dark", movesFor["Umbreon"], statsFor["Umbreon"], true));
        _pokemon.Add(new Pokemon("Hippowdon", "Ground", movesFor["Hippowdon"], statsFor["Hippowdon"], true));
        _pokemon.Add(new Pokemon("Sylveon", "Fairy", movesFor["Sylveon"], statsFor["Sylveon"], true));
        _pokemon.Add(new Pokemon("Nidoking", "Poison", movesFor["Nidoking"], statsFor["Nidoking"], true));
        _pokemon.Add(new Pokemon("Heracross", "Bug", movesFor["Heracross"], statsFor["Heracross"], true));










    }

    public static PokeDex GetPokeDex()
    {
        if (_instance == null)
        {
            lock (syncLock)
            {
                if (_instance == null)
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

