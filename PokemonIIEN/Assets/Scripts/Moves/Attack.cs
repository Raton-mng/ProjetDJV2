using System;
using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class Attack : Move
    {
        protected int BasePower;
        
        public Attack(PossibleTargets targets, Type type, int power, Pokemon assignedPokemon)
        {
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

        protected int Damage(Pokemon target)
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

    [CreateAssetMenu(fileName = "Attack", menuName = "Game/MoveDescription/Attack")]
    public class AttackDescription : MoveDescription
    {
        public int power;
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new Attack(targets, moveType, power, assignedPokemon);
        }
    }
}