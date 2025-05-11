using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildPokemon : Enemy
{
    private Pokemon _mySelf;

    public void Spawn(Pokemon pokemon)
    {
        _mySelf = pokemon;
    }
    
    public override Pokemon GetNonKoPokemon()
    {
        if (_mySelf.CurrentHp <= 0) return null;
        
        return _mySelf;
    }

    public (int, int) InverseCaptureRate()
    {
        //TODO Do the calculation
        return (1, 1);
    }
}
