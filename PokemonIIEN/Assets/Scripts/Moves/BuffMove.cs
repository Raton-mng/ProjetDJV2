using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class BuffMove : Move
    {
        private List<TargetedBuffNumber> _buffs;
        
        public override void DoSomething()
        {
            foreach (TargetedBuffNumber buff in _buffs)
            {
                List<Pokemon> buffTargets = CombatSingleton.CurrentCombat.GetTargets(AssignedPokemon, buff.target);
                foreach (Pokemon target in buffTargets)
                { 
                    CombatSingleton.CurrentCombat.AddPassiveMove(target, new BuffPassive(buff, target));
                }
            }
        }

        public BuffMove(Pokemon assignedPokemon, Type type, List<TargetedBuffNumber> buffs)
        {
            AssignedPokemon = assignedPokemon;
            MoveType = type;
            
            _buffs = buffs;
        }
    }

    [CreateAssetMenu(fileName = "Buff", menuName = "Game/MoveDescription/Buff")]
    public class BuffDescription : MoveDescription
    {
        public List<TargetedBuffNumber> buffs;
        
        public override Move CreateMove(Pokemon assignedPokemon)
        {
            return new BuffMove(assignedPokemon, moveType, buffs);
        }
    }
}
