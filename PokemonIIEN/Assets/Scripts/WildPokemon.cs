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

    public override void OnDefeat()
    {
        Destroy(gameObject);
    }

    public (int, int) CaptureRate(int pokeballPower)
    {
        // (i, j) = CaptureRate(power) correspond à i chances sur j de reussir
        return (pokeballPower, (int) _mySelf.HpPourcentage()); //on pourrait faire un meilleur calcul mais ceci fera l'affaire
    }
}
