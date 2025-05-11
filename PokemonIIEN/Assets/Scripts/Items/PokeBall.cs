using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public class PokeBall : ItemOnEnemy
{
    public int capturePower;

    public override bool UseOnEnemy(Pokemon enemyPokemon)
    {
        if (enemyPokemon.TryGetComponent(out WildPokemon pokemon))
        {
            CombatManager currentCombat = CombatSingleton.Instance.currentCombat;
            if (currentCombat != null)
            {
                (int, int) prob = pokemon.InverseCaptureRate();
                if (Random.Range(0, prob.Item2) < prob.Item1)
                {
                    currentCombat.Player.AddNewPokemon(pokemon.GetNonKoPokemon());
                }
                return true;
            }
            
            
            Debug.Log("Can't Use Outside Combat !!");
            return false;
        }
        
        Debug.Log("Can't Catch Trainer Pokemon !!");
        return false;
    }
}
