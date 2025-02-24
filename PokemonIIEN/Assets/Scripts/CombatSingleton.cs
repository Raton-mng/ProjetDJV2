using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;

public class CombatSingleton : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;

    public static CombatManager CurrentCombat;

    private void Awake()
    {
        CurrentCombat = Instantiate(combatManager);
    }

    //cette fonction est lancée quand on sait qu'il y a suffisament de pokemon pas ko
    public void NewCombat(List<Trainer> enemies, int pokemonsPerEnemy, List<Trainer> allies, int pokemonsPerAllies, Player player, int pokemonsForPlayer)
    {
        Destroy(combatManager);
        CurrentCombat = Instantiate(combatManager);
        
        //initiation des listes de pokemon du combat
        Dictionary<Pokemon, List<IPassiveMove>> pokemonOnField = new Dictionary<Pokemon, List<IPassiveMove>>();
        
        List<Pokemon> enemyPokemons = new List<Pokemon>();
        for (int i = 0; i < pokemonsPerEnemy; i++)
        {
            foreach (Trainer trainer in enemies)
            {
                Pokemon pokemon = trainer.GetNiemeNonKoPokemon(i);
                enemyPokemons.Add(pokemon);
                pokemonOnField.Add(pokemon, new List<IPassiveMove>());
            }
        }
        CurrentCombat.ennemies = enemyPokemons;
        
        List<Pokemon> allyPokemons = new List<Pokemon>();
        for (int i = 0; i < pokemonsPerAllies; i++)
        {
            foreach (Trainer trainer in allies)
            {
                Pokemon pokemon = trainer.GetNiemeNonKoPokemon(i);
                allyPokemons.Add(pokemon);
                pokemonOnField.Add(pokemon, new List<IPassiveMove>());
            }
        }
        CurrentCombat.allies = allyPokemons;
        
        List<Pokemon> playerPokemon = new List<Pokemon>();
        for (int i = 0; i < pokemonsForPlayer; i++)
        {
            Pokemon pokemon = player.GetNiemeNonKoPokemon(i);
            allyPokemons.Add(pokemon);
            pokemonOnField.Add(pokemon, new List<IPassiveMove>());
        }
        CurrentCombat.playerPokemons = playerPokemon;

        CurrentCombat.PokemonOnField = pokemonOnField;
    }
}
