using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public abstract class Move
     {
         public Pokemon AssignedPokemon;
         protected Type MoveType;
     
         public abstract void DoSomething();
     }
}

