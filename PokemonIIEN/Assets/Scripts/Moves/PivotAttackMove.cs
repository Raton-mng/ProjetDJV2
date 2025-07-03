using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class PivotAttackMove : Attack
    {
        public PivotAttackMove(PossibleTargets targets, Type moveType, int power, Pokemon assignedPokemon, string moveName): base(targets, moveType, power, assignedPokemon, moveName) {}
        
        public override void DoSomething()
        {
            CombatManager currentCombat = CombatSingleton.Instance.currentCombat;
            CombatUI currentUI = CombatSingleton.Instance.currentUI;
            
            List<Pokemon> attackTargets = currentCombat.GetTargets(AssignedPokemon, Targets);
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
                    moveText += ". ";
            }

            if (currentCombat.IsPlayerPokemon(AssignedPokemon))
            {
                if (currentCombat.Player.GetNonKoPokemon() == AssignedPokemon) currentUI.DisplayText(moveText);
                else currentUI.ChoosePokemon(moveText);
            }

            else
            {
                Pokemon newcomer = currentCombat.SwitchEnemyPokemon();
                if (newcomer) moveText += newcomer.nickname + " entered the field.";
                currentUI.DisplayText(moveText);
            }
        }
    }
}
