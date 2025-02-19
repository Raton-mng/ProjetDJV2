using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class BuffMove : Move
    {
        private List<BuffNumber> _buffs;
        
        public override void DoSomething(List<Pokemon> targets)
        {
            foreach (Pokemon target in targets)
            {
                foreach (BuffNumber buff in _buffs)
                {
                    CombatSingleton.CurrentCombat.AddBuff(target, new BuffPassive(buff, target));
                }
            }
        }

        public BuffMove(Pokemon assignedPokemon, PossibleTargets targets, Type type, List<BuffNumber> buffs)
        {
            AssignedPokemon = assignedPokemon;
            Targets = targets;
            MoveType = type;
            
            _buffs = buffs;
        }
    }

    [CreateAssetMenu(fileName = "Buff", menuName = "Game/MoveDescription/Buff")]
    public class BuffDescription : MoveDescription
    {
        public List<BuffNumber> buffs;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new BuffMove(assignedPokemon, targets, moveType, buffs);
        }
    }
}
