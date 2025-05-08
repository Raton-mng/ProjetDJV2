using System;
using System.Collections;
using System.Collections.Generic;
using Moves;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    public Dictionary<Pokemon, List<IPassiveMove>> PokemonOnField;
    
    //differentes sous-partie du plateau
    [HideInInspector] public Pokemon playerPokemon;
    [HideInInspector] public Pokemon enemyPokemon;

    [HideInInspector] public CombatUI ui;
    private List<Move> _movesThisTurn = new List<Move>();
    public Move selectedMove;

    private bool _finishingCombat;

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
            
            case PossibleTargets.Enemy :
                list = new List<Pokemon>();
                if (me == playerPokemon)
                    list.Add(enemyPokemon);
                else
                    list.Add(playerPokemon);
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

    public IEnumerator CombatLoop()
    {
        while (enemyPokemon.CurrentHp > 0 && playerPokemon.CurrentHp > 0)
        {
            selectedMove = null;
            //add enemy's move
            _movesThisTurn.Add(GetEnemyMove());
            //add player's move
            ui.ChooseMove();
            yield return new WaitUntil(() => selectedMove != null);
            _movesThisTurn.Add(selectedMove);

            foreach (Move move in _movesThisTurn)
            {
                print(move.moveName);
            }

            int j = _movesThisTurn.Count;
            for (int i = 0; i < j; i++)
            {
                Move move = GetNextMoveToPlay();
                if (move == null) continue;
                move.DoSomething();
                print(move.AssignedPokemon + " did : " + move.moveName);
            }
            
            print("player Hp : " + playerPokemon.CurrentHp);
            print("enemy Hp : " + enemyPokemon.CurrentHp);
            //pas fini ?
            //throw new NotImplementedException();
        }
        
        EndCombat(enemyPokemon.CurrentHp <= 0);
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

    private Move GetEnemyMove()
    {
        //description IA : 3 type de move :
        //  - attaque
        //  - buff
        //  - heal
        //Si l'ennemie a moins de 50% de sa vie, il va essayer de se heal s'il en a un.
        //Sinon, il choisi aléatoirement entre se buffer et attaquer avec son attaque la plus forte face à l'ennemie
        
        List<Move> buffMoves = new List<Move>();
        List<Move> healMoves = new List<Move>();
        List<Attack> attacks = new List<Attack>();
        foreach (Move enemyPokemonMove in enemyPokemon.Moves)
        {
            if (enemyPokemonMove is BuffMove) buffMoves.Add(enemyPokemonMove);
            switch (enemyPokemonMove)
            {
                case HealMove _ :
                    healMoves.Add(enemyPokemonMove);
                    break;
                case Attack attack :
                    attacks.Add(attack);
                    break;
                default: // sinon c'est un buff (ou soin/self dégats + buff)
                    buffMoves.Add(enemyPokemonMove);
                    break;
            }
        }

        if (healMoves.Count > 0 && enemyPokemon.CurrentHp <= enemyPokemon.BaseHp / 2)
        {
            return healMoves[Random.Range(0, healMoves.Count)];
        }

        if (attacks.Count > 0)
        {
            if (buffMoves.Count > 0)
            {
                Comparer<Attack> selectBestAttack = Comparer<Attack>.Create((x, y) => y.Damage(playerPokemon).CompareTo(x.Damage(playerPokemon))); //ordre décroissant de dégat
                attacks.Sort(selectBestAttack);
                
                if (Random.Range(0, 2) == 0) //on choisit une attaque
                    return attacks[0];
                
                //sinon on choisit un buff
                return buffMoves[Random.Range(0, buffMoves.Count)];
            }
            
            //si que attaque, alors on buff
            return attacks[0];
        }
        
        //sinon on a pas d'attaque (normalement on fera pas de pokemon comme ça)
        if (buffMoves.Count > 0)
            return buffMoves[Random.Range(0, buffMoves.Count)];

        //sinon il n'a que des soins (encore moins probable)
        return healMoves[Random.Range(0, healMoves.Count)];
        
        //et enfin si ça marche pas c'est que le pokemon n'a pas d'attaque et donc est bugué
    }
}
