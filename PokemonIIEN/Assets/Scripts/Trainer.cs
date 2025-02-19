using System.Collections.Generic;
using UnityEngine;

public class Trainer : MonoBehaviour
{
    private List<Pokemon> _party;
    
    public Pokemon GetNiemeNonKoPokemon(int n)
    {
        int i = -1;
        while (n > 0)
        {
            i++;
            if (i >= _party.Count)
            {
                print("no non-KO remaining Pokemon, what to do ?");
                return null;
            }
            
            if (_party[i].CurrentHp != 0) n -= 1;
        }

        return _party[i];
    }
}
