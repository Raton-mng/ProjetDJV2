using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class PriorityBuffMove : BuffMove, IPriorityMove
    {
        private int _priority;

        public int GetPriority()
        {
            return _priority;
        }

        public PriorityBuffMove(Pokemon assignedPokemon, Type type, List<TargetedBuffNumber> buffs, int priority) : base(assignedPokemon, type, buffs)
        {
            _priority = priority;
        }
    }
}
