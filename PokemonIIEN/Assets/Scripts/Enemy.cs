using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected List<PokeItem> rewardsItems;
    [SerializeField] protected List<int> rewardsAmount; //Les 2 listes doivent être dans le même ordre, avec le même nombre d'élément évidemment
    public Dictionary<PokeItem, int> items;

    protected void Awake()
    {
        if (rewardsItems.Count != rewardsAmount.Count)
            throw new ArgumentException("The 2 list are not equal in number");
        
        items = new Dictionary<PokeItem, int>();
        for (int i = 0; i < rewardsItems.Count; i++)
        {
            items.Add(rewardsItems[i], rewardsAmount[i]);
        }
    }

    public abstract Pokemon GetNonKoPokemon();

    public abstract void OnDefeat();
}
