using UnityEngine;

namespace Items
{
    public abstract class ItemOnAlly : Item
    {
        public abstract void UseOnAlly(Pokemon allyPokemon);
    }
}
