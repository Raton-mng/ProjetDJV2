using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class HealMove : Move
    {
        protected List<TargetedHealNumber> Heals;

        public HealMove(Pokemon assignedPokemon, Type type, List<TargetedHealNumber> heals, string thisMoveName, string thisMoveDescription)
        {
            AssignedPokemon = assignedPokemon;
            MoveType = type;
            
            Heals = heals;

            moveName = thisMoveName;

            moveDescription = thisMoveDescription;
        }
        
        public override void DoSomething()
        {
            CombatManager currentCombat = CombatSingleton.Instance.currentCombat;
            string moveText = AssignedPokemon.nickname + " used " + moveName + ".";
            foreach (TargetedHealNumber heal in Heals)
            {
                int healValue = GetHealValue(heal);
                List<Pokemon> buffTargets = currentCombat.GetTargets(AssignedPokemon, heal.target);
                foreach (Pokemon target in buffTargets)
                { 
                    currentCombat.AddPassiveMove(target, new HealPassive(healValue, heal.turnsBeforeStart, heal.duration, target, this, heal.isTransmissible));
                }
            }
            CombatSingleton.Instance.currentUI.DisplayText(moveText);
        }

        protected int GetHealValue(HealNumber heal)
        {
            return heal.fixedValue
                   + AssignedPokemon.CurrentHp * heal.hpPercentage / 100
                   + AssignedPokemon.CurrentAttack * heal.attackPercentage / 100
                   + AssignedPokemon.CurrentDefense * heal.defensePercentage / 100
                   + AssignedPokemon.CurrentSpeed * heal.speedPercentage / 100;
        }
    }
}
