using System;
using UnityEngine;

namespace Moves
{
    [Serializable]
    public class BuffNumber
    {
        public int turnsBeforeStart;
        [Min(1)] public int duration = 1;
        [Range(-4, 4)] public int attackBuff;
        [Range(-4, 4)] public int defenseBuff;
        [Range(-4, 4)] public int speedBuff;
    }

    [Serializable]
    public class TargetedBuffNumber : BuffNumber
    {
        public PossibleTargets target;
    }
}
