using System;
using System.Collections.Generic;
using Moves;
using UnityEngine;
using UnityEngine.Events;

public class CombatSingleton : MonoBehaviour
{
    public static CombatSingleton Instance;
    
    [SerializeField] private CombatManager combatManager;
    [SerializeField] private CombatUI combatUI; // doit être mis via sérialiser

    public CombatManager currentCombat;
    public CombatUI currentUI;
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
        
        if (player.GetNonKoPokemon() == null)
        {
            print("Can't Start, shouldn't happen");
            return;
        }
        
        Time.timeScale = 0;
        currentCombat = Instantiate(combatManager);
        currentUI = Instantiate(combatUI);
        
        Task t = new Task(currentCombat.CombatLoop(), false);
        
        currentUI.Initialize(player, enemy, player.items, t);
        currentCombat.Initialize(player, enemy, currentUI, t);
        
        t.Start();
    }
}
