using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "Attack", menuName = "Game/MoveDescription/Attack")]
    public class AttackDescription : MoveDescription
    {
        public PossibleTargets targets;
        public int power;
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new Attack(targets, moveType, power, assignedPokemon);
        }
    }
}
