using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Moves
{
    public class Attack : Move
    {
        protected int BasePower;
        protected PossibleTargets Targets;
        
        public Attack(PossibleTargets targets, Type type, int power, Pokemon assignedPokemon, string thisMoveName)
        {
            moveName = thisMoveName;
            Targets = targets;
            MoveType = type;
            BasePower = power;
            AssignedPokemon = assignedPokemon;
        }

        public override void DoSomething()
        {
            CombatSingleton combatSingleton = CombatSingleton.Instance;
            
            List<Pokemon> attackTargets = combatSingleton.currentCombat.GetTargets(AssignedPokemon, Targets);
            string moveText = AssignedPokemon.nickname + " used " + moveName + ".";
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
            
            combatSingleton.currentUI.DisplayText(moveText);
        }

        //return the damage, and the effiency of the attack
        public (float, float) Damage(Pokemon target)
        {
            float avantage = 1;
            foreach (Type targetType in target.Types)
            {
                if (MoveType.strongAgainst.Contains(targetType)) avantage *= 2;
                else if (MoveType.weakAgainst.Contains(targetType)) avantage /= 2;
            }
            
            return (BasePower * AssignedPokemon.CurrentAttack * avantage, avantage);
        }
    }
}