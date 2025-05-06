using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CombatUI : MonoBehaviour
{
    //differentes sous-partie du plateau
    [HideInInspector] public List<Pokemon> allies;
    [HideInInspector] public List<Pokemon> ennemies;
    [HideInInspector] public List<Pokemon> playerPokemons;

    public bool hasSelected;
    public Pokemon currentSelectedPokemon;

    public List<Pokemon> SelectSinglePokemon()
    {
        hasSelected = false;
        throw new NotImplementedException();
    }

    /*public async Task<List<Pokemon>> Test()
    {
        //return Task.Run(SelectSinglePokemon);
    }*/
}
