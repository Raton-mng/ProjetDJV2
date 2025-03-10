using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITmp : MonoBehaviour
{
    [SerializeField] private CombatSingleton cs;
    [SerializeField] private Player player;
    [SerializeField] private List<Trainer> enemyTrainers;
    [SerializeField] private List<Trainer> allyTrainers;
    [SerializeField] private int pokemonsPerEnemy;
    [SerializeField] private int pokemonsPerAllies;
    [SerializeField] private int pokemonsForPlayer;

    public void StartCombat()
    {
        cs.NewCombat(enemyTrainers, pokemonsPerEnemy, allyTrainers, pokemonsPerAllies, player, pokemonsForPlayer);
    }
}
