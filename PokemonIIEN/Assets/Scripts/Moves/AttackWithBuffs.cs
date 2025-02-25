using System;
using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class AttackWithBuffs : Attack
    {
        private List<TargetedBuffNumber> _buffsBeforeMove;
        private List<TargetedBuffNumber> _buffsAfterMove;

        public AttackWithBuffs(PossibleTargets targets, Type type, int power, Pokemon assignedPokemon,
            List<TargetedBuffNumber> buffsBeforeMove, List<TargetedBuffNumber> buffsAfterMove) : base(targets, type, power, assignedPokemon)
        {
            _buffsBeforeMove = buffsBeforeMove;
            _buffsAfterMove = buffsAfterMove;
        }
        
        public override void DoSomething()
        {
            foreach (TargetedBuffNumber buff in _buffsBeforeMove)
            {
                List<Pokemon> beforeTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, buff.target);
                foreach (Pokemon target in beforeTargets)
                {
                    CombatSingleton.CurrentCombat.AddPassiveMove(target, new BuffPassive(buff, target));
                }
            }
            
            List<Pokemon> attackTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, Targets);
            foreach (Pokemon target in attackTargets)
            {
                target.DealDamage(Damage(target));
            }
            
            foreach (TargetedBuffNumber buff in _buffsAfterMove)
            {
                List<Pokemon> beforeTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, buff.target);
                foreach (Pokemon target in beforeTargets)
                {
                    CombatSingleton.CurrentCombat.AddPassiveMove(target, new BuffPassive(buff, target));
                }
            }
        }
    }
}
