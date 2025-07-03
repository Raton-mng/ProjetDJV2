using System;
using UnityEngine;

namespace Moves
{
    [Serializable]
    public class BuffNumber
    {
        public bool isTransmissible;
        public int turnsBeforeStart;
        public int duration = 1;
        [Range(-10, 10)] public int attackBuff;
        [Range(-10, 10)] public int defenseBuff;
        [Range(-10, 10)] public int speedBuff;
    }

    [Serializable]
    public class TargetedBuffNumber : BuffNumber
    {
        public PossibleTargets target;
    }
}
