using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "Heal", menuName = "Game/MoveDescription/Heal")]
    public class HealDescription : MoveDescription
    {
        public List<TargetedHealNumber> heals;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new HealMove(assignedPokemon, moveType, heals);
        }
    }
}
