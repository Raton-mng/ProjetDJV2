using UnityEngine;

namespace Moves
{
    public abstract class PriorityMove : Move
    {
        public int Priority;
    }
    
    public abstract class PriorityMoveDescription : ScriptableObject
    {
        [Range(-6, 6)] public int priority;
    }
}
