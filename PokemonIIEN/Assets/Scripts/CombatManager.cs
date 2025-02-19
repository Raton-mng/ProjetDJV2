using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;
using UnityEngine.Serialization;

//chaque pokemon aura sera un de ces 4 types TODO : le rajouter dans la classe Pokemon
//sert à l'identification sur le terrain et savoir qu'est ce qu'on peut faire avec chacun
public enum StatusToPlayer
{
    Owned,
    Ally,
    Wild,
    EnemyOwned
}

public class CombatManager : MonoBehaviour
{
    public Dictionary<Pokemon, List<BuffPassive>> PokemonOnField;
    
    //liste des pokemons qui doivent encore jouer
    private List<Pokemon> _yetToPlayThisTurn;
    
    //differentes sous-partie du plateau
    public List<Pokemon> allies;
    public List<Pokemon> ennemies;
    public List<Pokemon> playerPokemons;

    public void AddBuff(Pokemon target, BuffPassive buff)
    {
        if (PokemonOnField.TryGetValue(target, out List<BuffPassive> currentList))
            currentList.Add(buff);
        else print("somehow, this target doesn't exist on the battlefield : " + target);
    }

    private Pokemon getNextPokemonToPlay()
    {
        Pokemon nextPokemon = null;
        int currentMaxSpeed = 0;
        foreach (Pokemon pokemon in _yetToPlayThisTurn)
        {
            int speed = pokemon.CurrentSpeed;
            if (speed >= currentMaxSpeed)
            {
                nextPokemon = pokemon;
                currentMaxSpeed = speed;
            }
        }

        _yetToPlayThisTurn.Remove(nextPokemon);
        return nextPokemon;
    }

    private void StartTurn()
    {
        bool hasWon = true;
        foreach (Pokemon enemy in ennemies)
        {
            if (enemy.CurrentHp != 0)
            {
                hasWon = false;
                break;
            }
        }
        if (hasWon)
        {
            EndCombat(true);
            return;
        }

        foreach (Pokemon ally in allies)
        {
            if (ally.CurrentHp != 0)
            {
                hasWon = true;
                break;
            }
        }
        if (!hasWon)
        {
            EndCombat(false);
            return;
        }

        _yetToPlayThisTurn = new List<Pokemon>(PokemonOnField.Keys);
    }

    private void EndCombat(bool hasWon)
    {
        
    }
}
