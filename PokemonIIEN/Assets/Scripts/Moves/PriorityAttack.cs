using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class PriorityAttack : Attack, IPriorityMove
    {
        private int _priority;

        public int GetPriority()
        {
            return _priority;
        }

        public PriorityAttack(PossibleTargets targets, Type type, int power, Pokemon assignedPokemon, int priority, string thisMoveName) : base(targets, type, power, assignedPokemon, thisMoveName)
        {
            Targets = targets;
            MoveType = type;
            BasePower = power;
            AssignedPokemon = assignedPokemon;
            _priority = priority;
        }
    }
}
