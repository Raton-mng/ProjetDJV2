using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Moves
{
    public class BuffMove : Move
    {
        private List<TargetedBuffNumber> _buffs;
        
        public override void DoSomething()
        {
            CombatManager currentCombat = CombatSingleton.Instance.currentCombat;
            string moveText = AssignedPokemon.nickname + " used " + moveName + ".";
            foreach (TargetedBuffNumber buff in _buffs)
            {
                List<Pokemon> buffTargets = currentCombat.GetTargets(AssignedPokemon, buff.target);
                foreach (Pokemon target in buffTargets)
                { 
                    currentCombat.AddPassiveMove(target, new BuffPassive(buff, target, this));
                }
            }
            CombatSingleton.Instance.currentUI.DisplayText(moveText);
        }

        public BuffMove(Pokemon assignedPokemon, Type type, List<TargetedBuffNumber> buffs, string thisMoveName, string thisMoveDescription)
        {
            AssignedPokemon = assignedPokemon;
            MoveType = type;
            
            _buffs = buffs;
            moveName = thisMoveName;

            moveDescription = thisMoveDescription;
        }
    }
}
