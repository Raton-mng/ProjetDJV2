namespace Moves
{
    public abstract class Move
    {
         public string moveName;
         public Pokemon AssignedPokemon;
         protected Type MoveType;
     
         public abstract void DoSomething();
     }
}

