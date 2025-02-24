using System;
using UnityEngine;

namespace Moves
{
    [Serializable]
    public class HealNumber
    {
        public int turnsBeforeStart;
        public int duration;
        public int fixedValue;
        [Range(0, 200)] public int hpPercentage;
        [Range(0, 200)] public int attackPercentage;
        [Range(0, 200)] public int defensePercentage;
        [Range(0, 200)] public int speedPercentage;
    }
    
    [Serializable]
    public class TargetedHealNumber : HealNumber
    {
        public PossibleTargets target;
    }
}
