using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class HealMove : Move
    {
        private List<TargetedHealNumber> _heals;

        public HealMove(Pokemon assignedPokemon, Type type, List<TargetedHealNumber> heals)
        {
            AssignedPokemon = assignedPokemon;
            MoveType = type;
            
            _heals = heals;
        }
        
        public override void DoSomething()
        {
            foreach (TargetedHealNumber heal in _heals)
            {
                int healValue = GetHealValue(heal);
                List<Pokemon> buffTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, heal.target);
                foreach (Pokemon target in buffTargets)
                { 
                    CombatSingleton.CurrentCombat.AddPassiveMove(target, new HealPassive(healValue, heal.turnsBeforeStart, heal.duration, target));
                }
            }
        }

        private int GetHealValue(HealNumber heal)
        {
            return heal.fixedValue
                   + AssignedPokemon.CurrentHp * heal.hpPercentage / 100
                   + AssignedPokemon.CurrentAttack * heal.attackPercentage / 100
                   + AssignedPokemon.CurrentDefense * heal.defensePercentage / 100
                   + AssignedPokemon.CurrentSpeed * heal.speedPercentage / 100;
        }
    }

    public class HealDescription : MoveDescription
    {
        public List<TargetedHealNumber> heals;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new HealMove(assignedPokemon, moveType, heals);
        }
    }
}
