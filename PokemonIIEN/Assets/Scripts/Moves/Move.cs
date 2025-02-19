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

    public abstract class Move
    {
        public Pokemon AssignedPokemon;
        public PossibleTargets Targets;
        protected Type MoveType;

        public abstract void DoSomething(List<Pokemon> targets);
    }

    public abstract class MoveDescription : ScriptableObject
    {
        public Type moveType;
        public PossibleTargets targets;
        public abstract Move CreateMove(Pokemon assignedPokemon);
    }
}