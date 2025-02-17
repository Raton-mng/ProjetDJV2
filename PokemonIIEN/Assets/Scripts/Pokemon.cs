using System;
using System.Collections.Generic;
using Items;
using Moves;
using UnityEngine;

public class Pokemon : MonoBehaviour
{
    public string nickname;

    [SerializeField] private int baseHp;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseDefense;
    [SerializeField] private int baseSpeed;
    public int BaseHp => baseHp;
    public int BaseAttack => baseAttack;
    public int BaseDefense => baseDefense;
    public int BaseSpeed => baseSpeed;

    public int CurrentHp { get; private set; }
    public int CurrentAttack { get; private set; }
    public int CurrentDefense { get; private set; }
    public int CurrentSpeed { get; private set; }

    [SerializeField] private List<Type> types;
    public List<Type> Types => types;

    [SerializeField] private List<int> dropQuantities;
    [SerializeField] private List<ItemOnAlly> drops;
    
    public Dictionary<IItem, int> Drops { get; private set; }

    [SerializeField] private List<MoveDescription> movesDescription;
    public List<Move> Moves { get  ; private set; }

    private void Awake()
    {
        CurrentHp = baseHp;
        CurrentAttack = baseAttack;
        CurrentDefense = baseDefense;
        CurrentSpeed = baseSpeed;

        Drops = new Dictionary<IItem, int>();
        for (int i = 0; i < drops.Count; i++)
        {
            Drops.Add(drops[i], dropQuantities[i]);
        }

        Moves = new List<Move>();
        foreach (MoveDescription mvd in movesDescription)
        {
            Moves.Add(mvd.CreateMove());
        }
    }

    public void HpChange(int value)
    {
        throw new NotImplementedException();
    }
}
