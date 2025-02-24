using System.Collections.Generic;
using UnityEngine;

namespace Moves
{

    [CreateAssetMenu(fileName = "BuffDescription", menuName = "Game/MoveDescription/BuffDescription")]
    public class BuffDescription : MoveDescription
    {
        public List<TargetedBuffNumber> buffs;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new BuffMove(assignedPokemon, moveType, buffs);
        }
    }
}
