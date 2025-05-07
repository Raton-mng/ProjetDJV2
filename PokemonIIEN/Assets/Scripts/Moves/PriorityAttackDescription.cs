using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "PriorityAttack", menuName = "Game/MoveDescription/PriorityAttack")]
    public class PriorityAttackDescription : PriorityMoveDescription
    {
        public int power;
        public PossibleTargets targets;
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new PriorityAttack(targets, moveType, power, assignedPokemon, priority, moveName);
        }
    }
}
