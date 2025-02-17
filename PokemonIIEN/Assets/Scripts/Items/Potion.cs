using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Potion", menuName = "Game/ItemOnAlly/Potion")]
    public class Potion : ItemOnAlly
    {
        public int healValue;

        public override void UseOnAlly(Pokemon allyPokemon)
        {
            allyPokemon.HpChange(healValue);
        }
    }
}
