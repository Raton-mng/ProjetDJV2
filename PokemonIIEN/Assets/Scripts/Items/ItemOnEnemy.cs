namespace Items
{
    public abstract class ItemOnEnemy : PokeItem
    {
        public abstract bool UseOnEnemy(Pokemon enemyPokemon); //return value indicates whether or not you can use the item 
    }
}
