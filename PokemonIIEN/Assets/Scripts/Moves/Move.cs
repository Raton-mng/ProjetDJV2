using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public enum PossibleTargets
    {
        Me,
        SingleTarget,
        AllEnemies,
        All
    }

    public abstract class Move
    {
        public PossibleTargets Targets;
        public Type Type;

        public abstract void DoSomething(List<Pokemon> targets);
    }

    public abstract class MoveDescription : ScriptableObject
    {
        public abstract Move CreateMove();
    }
}