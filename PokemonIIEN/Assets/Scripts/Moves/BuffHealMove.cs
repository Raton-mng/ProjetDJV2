using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class BuffHealMove : Move
    {
        private List<TargetedBuffHealNumber> _buffsHeals;
        
        public BuffHealMove(Pokemon assignedPokemon, Type type, List<TargetedBuffHealNumber> buffsHeals, string thisMoveName)
        {
            AssignedPokemon = assignedPokemon;
            MoveType = type;
            _buffsHeals = buffsHeals;
            moveName = thisMoveName;
        }
        
        public override void DoSomething()
        {
            foreach (TargetedBuffHealNumber buffHeal in _buffsHeals)
            {
                int healValue = GetHealValue(buffHeal);
                List<Pokemon> buffTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, buffHeal.target);
                foreach (Pokemon target in buffTargets)
                { 
                    CombatSingleton.CurrentCombat.AddPassiveMove(target, new BuffPassive(buffHeal, target));
                    CombatSingleton.CurrentCombat.AddPassiveMove(target, new HealPassive(healValue, buffHeal.turnsBeforeStart, buffHeal.duration, target));
                }
            }
        }
        
        private int GetHealValue(BuffHealNumber heal)
        {
            return heal.fixedValue
                   + AssignedPokemon.CurrentHp * heal.hpPercentage / 100
                   + AssignedPokemon.CurrentAttack * heal.attackPercentage / 100
                   + AssignedPokemon.CurrentDefense * heal.defensePercentage / 100
                   + AssignedPokemon.CurrentSpeed * heal.speedPercentage / 100;
        }
    }
}
