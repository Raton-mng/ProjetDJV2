using System;
using System.Collections.Generic;
using Items;
using Moves;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public string nickname;
    
    //Stat de base du pokemon
    [SerializeField] private int baseHp;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;
    [SerializeField] private int baseSpeed;
    public int BaseHp => baseHp;
    
    //stat effective du pokemon après buff/debuff (capé)
    public int CurrentHp { get; private set; }
    public int CurrentAttack { get; private set; }
    public int CurrentDefense { get; private set; }
    public int CurrentSpeed { get; private set; }
    
    //quantité de buff
    private int _boostAttack;
    private int _boostDefense;
    private int _boostSpeed;

    [SerializeField] private List<Type> types;
    public List<Type> Types => types;

    [SerializeField] private List<int> dropQuantities;
    [SerializeField] private List<Item> drops;
    public Dictionary<Item, int> Drops { get; private set; }

    [SerializeField] private List<MoveDescription> movesDescription;
    public List<Move> Moves { get  ; private set; }

    private void Awake()
    {
        CurrentHp = baseHp;
        CurrentAttack = baseAttack;
        CurrentDefense = baseDefense;
        CurrentSpeed = baseSpeed;

        _boostAttack = 0;
        _boostDefense = 0;
        _boostSpeed = 0;

        Drops = new Dictionary<Item, int>();
        for (int i = 0; i < drops.Count; i++)
        {
            Drops.Add(drops[i], dropQuantities[i]);
        }

        Moves = new List<Move>();
        foreach (MoveDescription mvd in movesDescription)
        {
            Moves.Add(mvd.CreateMove(this));
        }
    }

    public void HpChange(int value)
    {
        throw new NotImplementedException();
    }

    public void BoostAttack(int incrementValue)
    {
        if (incrementValue == 0) return;
        
        _boostAttack += incrementValue;
        if (_boostAttack < 0)
        {
            int actualNegativeBoost = Mathf.Min(4, -_boostAttack);
            CurrentAttack = Mathf.FloorToInt(baseAttack / (1 + 0.5f * actualNegativeBoost));
        }
        else
        {
            int actualPositiveBoost = Mathf.Max(4, _boostAttack);
            CurrentAttack = Mathf.FloorToInt(baseAttack * (1 + 0.5f * actualPositiveBoost));
        }
    }
    
    public void BoostDefense(int incrementValue)
    {
        if (incrementValue == 0) return;
        
        _boostDefense += incrementValue;
        if (_boostDefense < 0)
        {
            int actualNegativeBoost = Mathf.Min(4, -_boostDefense);
            CurrentDefense = Mathf.FloorToInt(_boostDefense / (1 + 0.5f * actualNegativeBoost));
        }
        else
        {
            int actualPositiveBoost = Mathf.Max(4, _boostDefense);
            CurrentDefense = Mathf.FloorToInt(_boostDefense * (1 + 0.5f * actualPositiveBoost));
        }
    }
    
    public void BoostSpeed(int incrementValue)
    {
        if (incrementValue == 0) return;
        
        _boostSpeed += incrementValue;
        if (_boostSpeed < 0)
        {
            int actualNegativeBoost = Mathf.Min(4, -_boostSpeed);
            CurrentSpeed = Mathf.FloorToInt(_boostSpeed / (1 + 0.5f * actualNegativeBoost));
        }
        else
        {
            int actualPositiveBoost = Mathf.Max(4, _boostSpeed);
            CurrentSpeed = Mathf.FloorToInt(_boostSpeed * (1 + 0.5f * actualPositiveBoost));
        }
    }

    public void DealDamage(int damage)
    {
        CurrentHp = Mathf.Max(0, CurrentHp - (damage /  (2 * CurrentDefense)));
    }
}
