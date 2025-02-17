using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class Attack : Move
    {

        private int _basePower;
        
        public Attack(PossibleTargets targets, Type type, int power)
        {
            Targets = targets;
            Type = type;
            _basePower = power;
        }

        public override void DoSomething(List<Pokemon> targets)
        {
            foreach (Pokemon target in targets)
            {
                target.HpChange(-_basePower);
            }
        }
    }

    [CreateAssetMenu(fileName = "Attack", menuName = "Game/MoveDescription/Attack")]
    public class AttackDescription : MoveDescription
    {
        public Type moveType;
        public PossibleTargets targets;
        public int power;
        
        public override Move CreateMove()
        {
            return new Attack(targets, moveType, power);
        }
    }
}