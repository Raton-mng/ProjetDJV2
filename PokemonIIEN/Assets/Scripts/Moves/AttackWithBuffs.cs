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
            List<TargetedBuffNumber> buffsBeforeMove, List<TargetedBuffNumber> buffsAfterMove, string thisMoveName) : base(targets, type, power, assignedPokemon, thisMoveName)
        {
            _buffsBeforeMove = buffsBeforeMove;
            _buffsAfterMove = buffsAfterMove;
        }
        
        public override void DoSomething()
        {
            CombatManager currentCombat = CombatSingleton.Instance.currentCombat;
            string moveText = AssignedPokemon.nickname + " used " + moveName + ".";
            foreach (TargetedBuffNumber buff in _buffsBeforeMove)
            {
                List<Pokemon> beforeTargets = currentCombat.GetTargets(AssignedPokemon, buff.target);
                foreach (Pokemon target in beforeTargets)
                {
                    CombatSingleton.Instance.currentCombat.AddPassiveMove(target, new BuffPassive(buff, target, this));
                }
            }
            
            List<Pokemon> attackTargets = currentCombat.GetTargets(AssignedPokemon, Targets);
            foreach (Pokemon target in attackTargets)
            {
                (float, float) attackContext = Damage(target);
                int damageInflicted = target.DealDamage(attackContext.Item1);
                moveText += " It dealt " + damageInflicted + " to " + target.nickname;
                if (attackContext.Item2 > 1.05)
                    moveText += " (super effective).";
                if (attackContext.Item2 < 0.95)
                    moveText += " (not very effective).";
                else
                    moveText += ".";
            }
            
            foreach (TargetedBuffNumber buff in _buffsAfterMove)
            {
                List<Pokemon> beforeTargets = currentCombat.GetTargets(AssignedPokemon, buff.target);
                foreach (Pokemon target in beforeTargets)
                {
                    currentCombat.AddPassiveMove(target, new BuffPassive(buff, target, this));
                }
            }
            
            CombatSingleton.Instance.currentUI.DisplayText(moveText);
        }
    }
}
