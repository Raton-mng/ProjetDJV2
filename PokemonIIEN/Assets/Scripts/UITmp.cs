using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITmp : MonoBehaviour
{
    [SerializeField] private CombatSingleton cs;
    [SerializeField] private Player player;
    [SerializeField] private Trainer enemyTrainer;

    public void StartCombat()
    {
        cs.NewCombat(enemyTrainer, player);
    }
}
