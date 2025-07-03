using System.Collections.Generic;

namespace Moves
{
    public class PivotAttackMove : Attack
    {
        public PivotAttackMove(PossibleTargets targets, Type moveType, int power, Pokemon assignedPokemon, string moveName): base(targets, moveType, power, assignedPokemon, moveName) {}
        
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
    }
}
