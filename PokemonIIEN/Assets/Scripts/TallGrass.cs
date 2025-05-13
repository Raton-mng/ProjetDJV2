using System;
using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;
using Random = UnityEngine.Random;

public class TallGrass : MonoBehaviour
{
    [SerializeField] private List<Pokemon> possibleEncounters;
    
    [SerializeField] private List<PokeItem> rewardsItems;
    [SerializeField] private List<int> rewardsAmount;
    private Dictionary<PokeItem, int> _items;
    
    private Player _player;
    private bool _isInside;
    private Vector3 _knownPlayerPosition;
    
    // prefab d'un gameObject vide avec la classe wildPokemon à instancier
    [SerializeField] private WildPokemon encounterPrefab;

    private void Awake()
    {
        if (rewardsItems.Count != rewardsAmount.Count)
            throw new ArgumentException("The 2 list are not equal in number");
        
        _items = new Dictionary<PokeItem, int>();
        for (int i = 0; i < rewardsItems.Count; i++)
        {
            _items.Add(rewardsItems[i], rewardsAmount[i]);
        }
    }

    private void Update()
    {
        // A chaque metre parcouru, 1 chance sur 4 d'avoir une rencontre sauvage
        if (_isInside && Vector3.Distance(_knownPlayerPosition, _player.transform.position) >= 4)
        {
            if (Random.Range(0, 4) == 0)
            {
                int pokeNumber = Random.Range(0, possibleEncounters.Count);
                StartEncounter(possibleEncounters[pokeNumber], _player);
            }

            _knownPlayerPosition = _player.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isInside = true;
            _player = player;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player _))
        {
            _isInside = false;
        }
    }

    private void StartEncounter(Pokemon pokemon, Player player)
    {
        WildPokemon encounter = Instantiate(encounterPrefab, transform);
        Pokemon encounterPoke = Instantiate(pokemon, encounter.transform);
        encounter.Spawn(encounterPoke);
        encounter.items = _items;
        
        CombatSingleton.Instance.NewCombat(encounter, player);
    }
}
