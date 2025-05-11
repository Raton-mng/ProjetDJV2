using UnityEngine;

namespace Moves
{
    public class HealPassive : IPassiveMove
    {
        private int _healValue;
        private Pokemon _assignedPokemon;

        private int _turnsBeforeStart;
        private int _duration;
        
        public HealPassive(int healValue, int turnsBeforeStart, int duration, Pokemon assignedPokemon)
        {
            _assignedPokemon = assignedPokemon;
            _healValue = healValue;
            
            _turnsBeforeStart = turnsBeforeStart;
            _duration = duration;
        }
        
        private void ApplyHeal()
        {
            if (_assignedPokemon.CurrentHp <= 0)
            {
                Debug.Log("nope");
                return;
            }
            _assignedPokemon.HpChange(_healValue);
            Debug.Log("iiiiiiiiiiiiih");
        }

        public bool DecrementDurations()
        {
            if (_turnsBeforeStart > 0)
            {
                _turnsBeforeStart -= 1;
                return false;
            }

            if (_duration > 0)
            {
                _duration -= 1;
                Debug.Log(_duration);
                ApplyHeal();
                return false;
            }

            return true;
        }

        public void EndMove() {}
    }
}
