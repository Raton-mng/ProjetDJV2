using UnityEngine;

namespace Moves
{
    public interface IPriorityMove
    {
        public int GetPriority();
    }
    
    public abstract class PriorityMoveDescription : MoveDescription
    {
        [Range(-6, 6)] public int priority;
    }
}
