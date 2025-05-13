using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;
using UnityEngine.Events;

public class CombatSingleton : MonoBehaviour
{
    public static CombatSingleton Instance;
    
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private CombatUI combatUI;

    public CombatManager currentCombat;
    public UnityEvent onCombatStart;

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
    public void NewCombat(Enemy enemy, Player player)
    {
        OverWorldUI.Instance.gameObject.SetActive(false);
        Cursor.visible = true;
        onCombatStart.Invoke();
        
        Pokemon playerPoke = player.GetNonKoPokemon();
        Pokemon enemyPoke = enemy.GetNonKoPokemon();
        if (playerPoke == null)
        {
            print("Can't Start, shouldn't happen");
            return;
        }
        
        Time.timeScale = 0;
        currentCombat = Instantiate(combatManager);
        CombatUI currentUI = Instantiate(combatUI);
        
        currentUI.Initialize(playerPoke, enemyPoke, player.items);
        currentCombat.Initialize(player, enemy, currentUI);
        
        StartCoroutine(currentCombat.CombatLoop());
    }
}
