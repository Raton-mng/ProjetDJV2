using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class PriorityAttack : Move, IPriorityMove
    {
        private int _priority;
        protected int BasePower;
        protected PossibleTargets Targets;

        public int GetPriority()
        {
            return _priority;
        }

        public PriorityAttack(PossibleTargets targets, Type type, int power, Pokemon assignedPokemon, int priority)
        {
            Targets = targets;
            MoveType = type;
            BasePower = power;
            AssignedPokemon = assignedPokemon;
            _priority = priority;
        }

        public override void DoSomething()
        {
            List<Pokemon> attackTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, Targets);
            foreach (Pokemon target in attackTargets)
            {
                target.DealDamage(Damage(target));
            }
        }
        
        protected int Damage(Pokemon target)
        {
            int avantage = 0;
            foreach (Type targetType in target.Types)
            {
                if (MoveType.strongAgainst.Contains(targetType)) avantage++;
                else if (MoveType.weakAgainst.Contains(targetType)) avantage--;
            }
            
            return BasePower * AssignedPokemon.CurrentAttack * avantage;
        }
    }

    [CreateAssetMenu(fileName = "PriorityAttack", menuName = "Game/MoveDescription/PriorityAttack")]
    public class PriorityAttackDescription : PriorityMoveDescription
    {
        public int power;
        public PossibleTargets targets;
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new PriorityAttack(targets, moveType, power, assignedPokemon, priority );
        }
    }
}
