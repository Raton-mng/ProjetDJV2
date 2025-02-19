using System;
using UnityEngine;

public class CombatSingleton : MonoBehaviour
{
    [SerializeField] private CombatManager combatManager;

    public static CombatManager CurrentCombat;

    private void Awake()
    {
        CurrentCombat = Instantiate(combatManager);
    }

    public void NewCombat()
    {
        Destroy(combatManager);
        CurrentCombat = Instantiate(combatManager);
    }
}
