using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Dictionary<Pokemon, List<IPassiveMove>> PokemonOnField;
    
    //liste des pokemons qui doivent encore jouer
    private List<Move> _movesThisTurn; //the move of the player this turn
    
    //differentes sous-partie du plateau
    [HideInInspector] public Pokemon playerPokemon;
    [HideInInspector] public Pokemon enemyPokemon;

    [HideInInspector] public CombatUI ui;
    private bool _isSelectingMove = false;
    
    public void AddPassiveMove(Pokemon target, IPassiveMove buff)
    {
        if (PokemonOnField.TryGetValue(target, out List<IPassiveMove> currentList))
            currentList.Add(buff);
        else print("somehow, this target doesn't exist on the battlefield : " + target);
    }

    public List<Pokemon> GetTargets(Pokemon me, PossibleTargets possibleTargets)
    {
        throw new NotImplementedException();
    }
    
    /*public List<Pokemon> GetTargets(Pokemon me, PossibleTargets possibleTargets)
    {
        //a la base je voulais faire comme assertable avec AsyncOperationHandle mais j'y suis pas encore arriver attention (s'il faut remettre en fonction normale)
        _isSelectingMove = true;
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
                list = new List<Pokemon>();
                /*StartCoroutine(ui.SelectSinglePokemon());
                while (!ui.hasSelected) yield return null;
                list.Add(ui.currentSelectedPokemon);/
                await ui.Test();
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
        _isSelectingMove = false;
        return list;
    }*/

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
        if (enemyPokemon.CurrentHp <= 0)
            EndCombat(true);


        if (playerPokemon.CurrentHp <= 0)
            EndCombat(false);

        _movesThisTurn = GetMovesOfThisTurn();
        //pas fini
    }

    private void EndCombat(bool hasWon)
    {
        foreach (var pokemonsBuff in PokemonOnField)
        {
            foreach (IPassiveMove pMove in pokemonsBuff.Value)
            {
                pMove.EndMove();
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
