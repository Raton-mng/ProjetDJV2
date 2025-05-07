using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "BuffHeal", menuName = "Game/MoveDescription/BuffHeal")]
    public class BuffHealDescription : MoveDescription
    {
        public List<TargetedBuffHealNumber> buffsHeals;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new BuffHealMove(assignedPokemon, moveType, buffsHeals, moveName);
        }
    }
}
