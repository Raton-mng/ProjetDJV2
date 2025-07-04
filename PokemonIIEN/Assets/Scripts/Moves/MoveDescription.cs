using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public enum PossibleTargets
    {
        Me,
        Enemy,
        All
    }

    public abstract class MoveDescription : ScriptableObject
    {
        public Type moveType;
        public string moveName;
        public abstract Move CreateMove(Pokemon assignedPokemon);
        
        public string moveDescription;
    }
}