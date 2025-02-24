using System;
using UnityEngine;

namespace Moves
{
    [Serializable]
    public class BuffHealNumber : BuffNumber
    {
        public int turnsBeforeHealStart;
        public int healDuration = 1;
        public int fixedValue;
        [Range(-100, 200)] public int hpPercentage;
        [Range(0, 200)] public int attackPercentage;
        [Range(0, 200)] public int defensePercentage;
        [Range(0, 200)] public int speedPercentage;
    }

    [Serializable]
    public class TargetedBuffHealNumber : BuffHealNumber
    {
        public PossibleTargets target;
    }
}
