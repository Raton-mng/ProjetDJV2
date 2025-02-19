using System.Collections.Generic;
using Moves;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private Dictionary<Pokemon, List<BuffPassive>> _pokemonOnField;

    public void AddBuff(Pokemon target, BuffPassive buff)
    {
        if (_pokemonOnField.TryGetValue(target, out List<BuffPassive> currentList))
            currentList.Add(buff);
        else print("somehow, this target doesn't exist on the battlefield : " + target);
    }
}
