using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class HealMove : Move
    {
        protected List<TargetedHealNumber> Heals;

        public HealMove(Pokemon assignedPokemon, Type type, List<TargetedHealNumber> heals)
        {
            AssignedPokemon = assignedPokemon;
            MoveType = type;
            
            Heals = heals;
        }
        
        public override void DoSomething()
        {
            foreach (TargetedHealNumber heal in Heals)
            {
                int healValue = GetHealValue(heal);
                List<Pokemon> buffTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, heal.target);
                foreach (Pokemon target in buffTargets)
                { 
                    CombatSingleton.CurrentCombat.AddPassiveMove(target, new HealPassive(healValue, heal.turnsBeforeStart, heal.duration, target));
                }
            }
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

    [CreateAssetMenu(fileName = "Heal", menuName = "Game/MoveDescription/Heal")]
    public class HealDescription : MoveDescription
    {
        public List<TargetedHealNumber> heals;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new HealMove(assignedPokemon, moveType, heals);
        }
    }
}
