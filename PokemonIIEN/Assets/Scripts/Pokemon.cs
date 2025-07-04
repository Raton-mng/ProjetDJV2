using System;
using System.Collections.Generic;
using Items;
using Moves;
using UnityEngine;
using UnityEngine.Events;

public class Pokemon : MonoBehaviour
{
    public string nickname;
    
    //Stat de base du pokemon
    [SerializeField] private int baseHp;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;
    [SerializeField] private int baseSpeed;
    
    //stat effective du pokemon après buff/debuff (capé)
    public int CurrentHp { get; private set; }
    public int CurrentAttack { get; private set; }
    public int CurrentDefense { get; private set; }
    public int CurrentSpeed { get; private set; }
    
    //quantité de buff
    private int _boostAttack;
    private int _boostDefense;
    private int _boostSpeed;
    public string actualBoostAttack = "ATK + 0";
    public string actualBoostDefense = "DEF + 0";
    public string actualBoostSpeed = "SPD + 0";

    [SerializeField] private List<Type> types;
    public List<Type> Types => types;

    [SerializeField] private List<MoveDescription> movesDescription;
    public List<Move> Moves { get  ; private set; }

    public UnityEvent<float> hpChanged = new UnityEvent<float>();

    private void Awake()
    {
        CurrentHp = baseHp;
        CurrentAttack = baseAttack;
        CurrentDefense = baseDefense;
        CurrentSpeed = baseSpeed;

        _boostAttack = 0;
        _boostDefense = 0;
        _boostSpeed = 0;

        Moves = new List<Move>();
        foreach (MoveDescription mvd in movesDescription)
        {
            Moves.Add(mvd.CreateMove(this));
        }
    }

    public void HpChange(int value)
    {
        CurrentHp = Mathf.Clamp(CurrentHp + value, 1, baseHp);
        hpChanged.Invoke(HpPourcentage());
    }

    public void HealToMax()
    {
        print(gameObject);
        print(transform.parent.gameObject);
        print(CurrentHp);
        HpChange(baseHp);
        print(CurrentHp);
    }

    public void BoostAttack(int incrementValue)
    {
        if (incrementValue == 0) return;
        
        _boostAttack += incrementValue;
        if (_boostAttack < 0)
        {
            int actualNegativeBoost = Mathf.Min(4, -_boostAttack);
            actualBoostAttack = "ATK - " + actualNegativeBoost;
            CurrentAttack = Mathf.FloorToInt(baseAttack / (1 + 0.5f * actualNegativeBoost));
        }
        else
        {
            int actualPositiveBoost = Mathf.Min(4, _boostAttack);
            actualBoostAttack = "ATK + " + actualPositiveBoost;
            CurrentAttack = Mathf.FloorToInt(baseAttack * (1 + 0.5f * actualPositiveBoost));
        }
        //print("pokemon : " + nickname + "; increment : " + incrementValue + "; _boostAttack : " + _boostAttack);
    }
    
    public void BoostDefense(int incrementValue)
    {
        if (incrementValue == 0) return;
        
        _boostDefense += incrementValue;
        if (_boostDefense < 0)
        {
            int actualNegativeBoost = Mathf.Min(4, -_boostDefense);
            actualBoostDefense = "DEF - " + actualNegativeBoost;
            CurrentDefense = Mathf.FloorToInt(baseDefense / (1 + 0.5f * actualNegativeBoost));
        }
        else
        {
            int actualPositiveBoost = Mathf.Min(4, _boostDefense);
            actualBoostDefense = "DEF + " + actualPositiveBoost;
            CurrentDefense = Mathf.FloorToInt(baseDefense * (1 + 0.5f * actualPositiveBoost));
        }
        //print("pokemon : " + nickname + "; increment : " + incrementValue + "; _boostDefense : " + _boostDefense);
    }
    
    public void BoostSpeed(int incrementValue)
    {
        if (incrementValue == 0) return;
        
        _boostSpeed += incrementValue;
        if (_boostSpeed < 0)
        {
            int actualNegativeBoost = Mathf.Min(4, -_boostSpeed);
            actualBoostSpeed = "SPD - " + actualNegativeBoost;
            CurrentSpeed = Mathf.FloorToInt(baseSpeed / (1 + 0.5f * actualNegativeBoost));
        }
        else
        {
            int actualPositiveBoost = Mathf.Min(4, _boostSpeed);
            actualBoostSpeed = "SPD + " + actualPositiveBoost;
            CurrentSpeed = Mathf.FloorToInt(baseSpeed * (1 + 0.5f * actualPositiveBoost));
        }
        //print("pokemon : " + nickname + "; increment : " + incrementValue + "; _boostSpeed : " + _boostSpeed);
    }
    
    //return the damage inflicted to the pokemon
    public int DealDamage(float damage)
    {
        int damageInflicted = Mathf.FloorToInt(damage /  (2.5f * CurrentDefense));
        CurrentHp = Mathf.Max(0,  CurrentHp - damageInflicted);
        hpChanged.Invoke(HpPourcentage());
        return damageInflicted;
    }

    public float HpPourcentage()
    {
        return CurrentHp / (float) baseHp;
    }

    public int GetBaseHp()
    {
        return baseHp;
    }
}