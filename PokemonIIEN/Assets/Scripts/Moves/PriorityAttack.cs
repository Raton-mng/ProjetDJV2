using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class PriorityAttack : PriorityMove
    {
        private int _basePower;

        public PriorityAttack(PossibleTargets targets, Type type, int power, Pokemon assignedPokemon, int priority)
        {
            Targets = targets;
            MoveType = type;
            _basePower = power;
            AssignedPokemon = assignedPokemon;
            Priority = priority;
        }

        public override void DoSomething(List<Pokemon> targets)
        {
            
        }
    }

    [CreateAssetMenu(fileName = "PriorityAttack", menuName = "Game/MoveDescription/PriorityPriorityAttack")]
    public class PriorityAttackDescription : PriorityMoveDescription
    {
        public int power;
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new PriorityAttack(targets, moveType, power, assignedPokemon, priority );
        }
    }
}
