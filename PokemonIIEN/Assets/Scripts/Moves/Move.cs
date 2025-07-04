namespace Moves
{
    public abstract class Move
    {
         public string moveName;
         public Pokemon AssignedPokemon;
         public Type MoveType;
         
         public string moveDescription;
     
         public abstract void DoSomething();
     }
}

