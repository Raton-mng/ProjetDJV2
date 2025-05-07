using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "AttackWithBuffs", menuName = "Game/MoveDescription/AttackWithBuffs")]
    public class AttackWithBuffsDescription : AttackDescription
    {
        public List<TargetedBuffNumber> buffsBeforeMove;
        public List<TargetedBuffNumber> buffsAfterMove;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new AttackWithBuffs(targets, moveType, power, assignedPokemon, buffsBeforeMove, buffsAfterMove, moveName);
        }
    }
}
