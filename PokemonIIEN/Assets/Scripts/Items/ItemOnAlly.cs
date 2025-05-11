using UnityEngine;

namespace Items
{
    public abstract class ItemOnAlly : PokeItem
    {
        public abstract bool UseOnAlly(Pokemon allyPokemon); //return value indicates whether or not you can use the item 
    }
}
