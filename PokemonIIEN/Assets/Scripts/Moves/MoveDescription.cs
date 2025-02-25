using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public enum PossibleTargets
    {
        Me,
        AllAllies,
        SingleTarget,
        AllEnemies,
        All
    }

    public abstract class MoveDescription : ScriptableObject
    {
        public Type moveType;
        public abstract Move CreateMove(Pokemon assignedPokemon);
    }
}