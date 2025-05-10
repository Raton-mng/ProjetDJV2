using System;
using System.Collections;
using System.Collections.Generic;
using Moves;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour
{
    private Dictionary<Pokemon, List<IPassiveMove>> _pokemonOnField;
    
    //differentes sous-partie du plateau
    private Player _player;
    private Trainer _enemy;
    
    public Pokemon PlayerPokemon { get; private set; }
    private Pokemon _enemyPokemon;

    private CombatUI _ui;
    private List<Move> _movesThisTurn;
    public Move selectedMove;

    private bool _finishingCombat;

    public void Initialize(Player player, Trainer enemyTrainer, CombatUI combatUI)
    {
        _player = player;
        _enemy = enemyTrainer;
        
        PlayerPokemon = _player.GetNiemeNonKoPokemon(0);
        _enemyPokemon = _enemy.GetNiemeNonKoPokemon(0);
        _ui = combatUI;

        //initiation des listes de pokemon du combat
        _pokemonOnField = new Dictionary<Pokemon, List<IPassiveMove>>();
        _pokemonOnField.Add(PlayerPokemon, new List<IPassiveMove>());
        _pokemonOnField.Add(_enemyPokemon, new List<IPassiveMove>());
        
        if (PlayerPokemon == null || _enemyPokemon == null)
        {
            print("ERROR IN STARTING COMBAT : no pokemon available");
            throw new ArgumentNullException();
        }
        
        _movesThisTurn = new List<Move>();
    }

    public void AddPassiveMove(Pokemon target, IPassiveMove buff)
    {
        if (_pokemonOnField.TryGetValue(target, out List<IPassiveMove> currentList))
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
                if (me == PlayerPokemon)
                    list.Add(_enemyPokemon);
                else
                    list.Add(PlayerPokemon);
                break;
            default:
                list = new List<Pokemon>(_pokemonOnField.Keys);
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
        while (_enemyPokemon.CurrentHp > 0 && PlayerPokemon.CurrentHp > 0)
        {
            _movesThisTurn.Clear();
            selectedMove = null;
            //add enemy's move
            _movesThisTurn.Add(GetEnemyMove());
            //add player's move
            _ui.ChooseMove();
            yield return new WaitUntil(() => selectedMove != null);
            _movesThisTurn.Add(selectedMove);

            int j = _movesThisTurn.Count;
            print(j);
            for (int i = 0; i < j; i++)
            {
                Move move = GetNextMoveToPlay();
                print(move);
                if (move == null) continue;
                move.DoSomething();
            }

            foreach (var pokemonPassifs in _pokemonOnField)
            {
                List<IPassiveMove> toRemove = new List<IPassiveMove>();
                foreach (IPassiveMove passif in pokemonPassifs.Value)
                {
                    if (passif.DecrementDurations())
                    {
                        toRemove.Add(passif);
                    }
                }

                foreach (IPassiveMove passif in toRemove)
                {
                    pokemonPassifs.Value.Remove(passif);
                }
            }
            //pas fini ?
            //throw new NotImplementedException();
        }

        if (_enemyPokemon.CurrentHp <= 0)
        {
            Pokemon nextEnemy = _enemy.GetNiemeNonKoPokemon(0);
            print(nextEnemy);
            if (nextEnemy == null)
            {
                EndCombat(true);
            }
            else
            {
                _pokemonOnField.Remove(_enemyPokemon); _pokemonOnField.Add(nextEnemy, new List<IPassiveMove>());
                _enemyPokemon = nextEnemy;
                StartCoroutine(CombatLoop());
            }
        }
        else
        {
            Pokemon nextPlayerPokemon = _player.GetNiemeNonKoPokemon(0);
            print(nextPlayerPokemon);
            if (nextPlayerPokemon == null)
            {
                EndCombat(false);
            }
            else
            {
                _pokemonOnField.Remove(PlayerPokemon); _pokemonOnField.Add(nextPlayerPokemon, new List<IPassiveMove>());
                PlayerPokemon = nextPlayerPokemon;
                _ui.UpdatePokemons();
                StartCoroutine(CombatLoop());
            }
        }
    }

    private void EndCombat(bool hasWon)
    {
        foreach (var pokemonsBuff in _pokemonOnField)
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
        foreach (Move enemyPokemonMove in _enemyPokemon.Moves)
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

        if (healMoves.Count > 0 && _enemyPokemon.CurrentHp <= _enemyPokemon.BaseHp / 2)
        {
            return healMoves[Random.Range(0, healMoves.Count)];
        }

        if (attacks.Count > 0)
        {
            Comparer<Attack> selectBestAttack = Comparer<Attack>.Create((x, y) => y.Damage(PlayerPokemon).CompareTo(x.Damage(PlayerPokemon))); //ordre décroissant de dégat
            attacks.Sort(selectBestAttack);
            if (buffMoves.Count > 0)
            {
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
