using UnityEngine;

namespace Items
{
    public abstract class ItemOnAlly : ScriptableObject, IItem
    {
        public abstract void UseOnAlly(Pokemon allyPokemon);
    }
}
