using System;
using System.Collections.Generic;
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
            List<Pokemon> attackTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, Targets);
            foreach (Pokemon target in attackTargets)
            {
                target.DealDamage(Damage(target));
            }
        }

        public int Damage(Pokemon target)
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
}