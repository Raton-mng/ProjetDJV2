using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;

public class CombatSingleton : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private CombatUI combatUI;

    public static CombatManager CurrentCombat;

    /*private void Awake()
    {
        //CurrentCombat = Instantiate(combatManager);
    }*/

    //cette fonction est lancée quand on sait qu'il y a suffisament de pokemon pas ko
    public void NewCombat(Trainer enemy, Player player)
    {
        Destroy(CurrentCombat);
        CurrentCombat = Instantiate(combatManager);
        CombatUI currentUI = Instantiate(combatUI);
        
        currentUI.Initialize(player.GetNiemeNonKoPokemon(0), enemy.GetNiemeNonKoPokemon(0));
        
        CurrentCombat.Initialize(player, enemy, currentUI);
        
        StartCoroutine(CurrentCombat.CombatLoop());
    }
}
