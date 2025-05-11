using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Trainer))]
public class EnemyDetectorPlayer : MonoBehaviour
{
    private Trainer _myTeam;
    
    private void Awake()
    {
        _myTeam = GetComponent<Trainer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other);
        if (other.TryGetComponent(out Player player))
        {
            CombatSingleton.Instance.NewCombat(_myTeam, player);
        }
    }
}
