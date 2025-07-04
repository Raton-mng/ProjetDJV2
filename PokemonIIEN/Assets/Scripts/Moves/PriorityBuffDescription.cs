using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "PriorityBuff", menuName = "Game/MoveDescription/PriorityBuff")]
    public class PriorityBuffDescription : PriorityMoveDescription
    {
        public List<TargetedBuffNumber> buffs;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new PriorityBuffMove(assignedPokemon, moveType, buffs, priority, moveName, moveDescription);
        }
    }
}
