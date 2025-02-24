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
    public Dictionary<Pokemon, List<IPassiveMove>> PokemonOnField;
    
    //liste des pokemons qui doivent encore jouer
    private List<Move> _movesThisTurn;
    
    //differentes sous-partie du plateau
    public List<Pokemon> allies;
    public List<Pokemon> ennemies;
    public List<Pokemon> playerPokemons;

    public void AddPassiveMove(Pokemon target, IPassiveMove buff)
    {
        if (PokemonOnField.TryGetValue(target, out List<IPassiveMove> currentList))
            currentList.Add(buff);
        else print("somehow, this target doesn't exist on the battlefield : " + target);
    }

    public List<Pokemon> GetTargets(Pokemon me, PossibleTargets possibleTargets)
    {
        List<Pokemon> list;
        switch (possibleTargets)
        {
            case PossibleTargets.Me :
                list = new List<Pokemon>();
                list.Add(me);
                break;
            
            case PossibleTargets.AllAllies :
                if (ennemies.Contains(me))
                    list = new List<Pokemon>(ennemies);
                else
                {
                    list = new List<Pokemon>(allies);
                    list.AddRange(playerPokemons);
                }
                break;
            
            case PossibleTargets.SingleTarget :
                throw new NotImplementedException();
                break;
            
            case PossibleTargets.AllEnemies :
                if (ennemies.Contains(me))
                {
                    list = new List<Pokemon>(allies);
                    list.AddRange(playerPokemons);
                }
                else
                    list = new List<Pokemon>(ennemies);
                break;
            default:
                list = new List<Pokemon>(PokemonOnField.Keys);
                break;
        }
        return list;
    } 

    private Move GetNextMoveToPlay()
    {
        Move nextMove = null;
        int currentMaxSpeed = 0;
        int currentMaxPriority = 0;
        foreach (Move move in _movesThisTurn)
        {
            if (move.AssignedPokemon.CurrentHp == 0) continue;
            
            int priority = move is IPriorityMove ? ((IPriorityMove) move).GetPriority() : 0;
            int speed = move.AssignedPokemon.CurrentSpeed;
            if (priority >= currentMaxPriority)
            {
                if (priority > currentMaxPriority || speed >= currentMaxSpeed)
                {
                    nextMove = move;
                    currentMaxSpeed = speed;
                }
            }
        }

        _movesThisTurn.Remove(nextMove);
        return nextMove;
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

        _movesThisTurn = GetMovesOfThisTurn();
        //pas fini
    }

    private void EndCombat(bool hasWon)
    {
        foreach (var pokemonsBuff in PokemonOnField)
        {
            foreach (BuffPassive buff in pokemonsBuff.Value)
            {
                buff.EndMove();
            }
        }
        //pas fini
        throw new NotImplementedException();
    }

    private List<Move> GetMovesOfThisTurn()
    {
        throw new NotImplementedException();
    }
}
