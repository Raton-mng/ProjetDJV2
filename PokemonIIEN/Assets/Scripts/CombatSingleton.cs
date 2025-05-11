using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;

public class CombatSingleton : MonoBehaviour
{
    public static CombatSingleton Instance;
    
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private CombatUI combatUI;

    public static CombatManager CurrentCombat;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        } 
        
        Instance = this;
        DontDestroyOnLoad(this);
    }

    //cette fonction est lancée quand on sait qu'il y a suffisament de pokemon pas ko
    public void NewCombat(Trainer enemy, Player player)
    {
        Pokemon playerPoke = player.GetNiemeNonKoPokemon(0);
        Pokemon enemyPoke = enemy.GetNiemeNonKoPokemon(0);
        if (playerPoke == null || enemyPoke == null)
        {
            print("Can't Start");
            return;
        }
        
        Time.timeScale = 0;
        Destroy(CurrentCombat);
        CurrentCombat = Instantiate(combatManager);
        CombatUI currentUI = Instantiate(combatUI);
        
        currentUI.Initialize(playerPoke, enemyPoke);
        
        CurrentCombat.Initialize(player, enemy, currentUI);
        
        StartCoroutine(CurrentCombat.CombatLoop());
    }
}
