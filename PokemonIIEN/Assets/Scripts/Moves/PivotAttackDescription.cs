using UnityEngine;

namespace Moves
{
    [CreateAssetMenu(fileName = "PivotAttack", menuName = "Game/MoveDescription/PivotAttack")]
    public class PivotAttackDescription : AttackDescription
    {
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new PivotAttackMove(targets, moveType, power, assignedPokemon, moveName);
        }
    }
}
