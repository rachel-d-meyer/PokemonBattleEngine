﻿

public struct ActivePokemon {
    public Pokemon P;
    public bool Player;
	public ActivePokemon(Pokemon p, bool player)
    {
        P = p;
        Player = player;
    }
}
