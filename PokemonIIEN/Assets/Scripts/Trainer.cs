using System;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class Trainer : Enemy
{
    [SerializeField] private List<Pokemon> party;
    protected List<Pokemon> instantiatedParty;

    public bool wasBeaten;

    private new void Awake()
    {
        base.Awake();
        
        instantiatedParty = new List<Pokemon>();
        
        foreach (Pokemon pokemon in party)
        {
            Pokemon instantiatedPokemon = Instantiate(pokemon, transform);
            instantiatedPokemon.gameObject.SetActive(false);
            instantiatedParty.Add(instantiatedPokemon);
        }
    }

    public override Pokemon GetNonKoPokemon()
    {
        int i = 0;
        while (i < instantiatedParty.Count)
        {
            Pokemon pokemon = instantiatedParty[i];
            if (pokemon.CurrentHp != 0) return pokemon;

            i++;
        }

        print("no non-KO remaining Pokemon, what to do ?");
        return null;
    }

    public void HealAllPokemons()
    {
        foreach (var pokemon in instantiatedParty)
        {
            pokemon.HealToMax();
        }
    }

    public override void OnDefeat()
    {
        wasBeaten = true;
    }
}
