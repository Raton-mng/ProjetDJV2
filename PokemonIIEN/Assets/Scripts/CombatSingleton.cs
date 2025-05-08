using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;

public class CombatSingleton : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private CombatUI combatUI;
    
    //temp :
    [SerializeField] private Transform canva;

    public static CombatManager CurrentCombat;

    /*private void Awake()
    {
        //CurrentCombat = Instantiate(combatManager);
    }*/

    //cette fonction est lancée quand on sait qu'il y a suffisament de pokemon pas ko
    public void NewCombat(Trainer enemy, Player player)
    {
        Destroy(CurrentCombat);
        CurrentCombat = Instantiate(combatManager);
        CombatUI currentUI = Instantiate(combatUI, canva);
        CurrentCombat.ui = currentUI;
        
        //initiation des listes de pokemon du combat
        Dictionary<Pokemon, List<IPassiveMove>> pokemonOnField = new Dictionary<Pokemon, List<IPassiveMove>>();
        
        Pokemon enemyPokemon = enemy.GetNiemeNonKoPokemon(0);
        pokemonOnField.Add(enemyPokemon, new List<IPassiveMove>());
        CurrentCombat.enemyPokemon = enemyPokemon;
        
        Pokemon playerPokemon = player.GetNiemeNonKoPokemon(0);
        pokemonOnField.Add(playerPokemon, new List<IPassiveMove>());
        CurrentCombat.playerPokemon = playerPokemon;

        CurrentCombat.PokemonOnField = pokemonOnField;
        
        currentUI.playerPokemon = playerPokemon;
        currentUI.enemyPokemon = enemyPokemon;
        currentUI.combatManager = CurrentCombat;
        currentUI.Something();
        

        StartCoroutine(CurrentCombat.CombatLoop());
    }
}
