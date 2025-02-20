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

        public override void DoSomething(List<Pokemon> targets)
        {
            foreach (Pokemon target in targets)
            {
                throw new NotImplementedException();
            }
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