using System;
using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    public List<Pokemon> party;
    private List<Pokemon> _instantiatedParty;

    private void Awake()
    {
        _instantiatedParty = new List<Pokemon>();
        
        foreach (Pokemon pokemon in party)
        {
            Pokemon instantiatedPokemon = Instantiate(pokemon);
            instantiatedPokemon.gameObject.SetActive(false);
            _instantiatedParty.Add(instantiatedPokemon);
        }
    }

    public Pokemon GetNiemeNonKoPokemon(int n)
    {
        int i = -1;
        while (n >= 0)
        {
            i++;
            if (i >= _instantiatedParty.Count)
            {
                print("no non-KO remaining Pokemon, what to do ?");
                return null;
            }
            
            if (_instantiatedParty[i].CurrentHp != 0) n -= 1;
        }

        return _instantiatedParty[i];
    }
}
