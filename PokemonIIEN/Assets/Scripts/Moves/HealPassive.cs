using UnityEngine;

namespace Moves
{
    public class HealPassive : IPassiveMove
    {
        private bool _isTransmissible;
        private int _healValue;
        private Pokemon _assignedPokemon;
        private Move _associatedMove;

        private int _turnsBeforeStart;
        private int _duration;
        
        public HealPassive(int healValue, int turnsBeforeStart, int duration, Pokemon assignedPokemon, Move associatedMove, bool isTransmissible)
        {
            _assignedPokemon = assignedPokemon;
            _healValue = healValue;
            
            _turnsBeforeStart = turnsBeforeStart;
            _duration = duration;

            _associatedMove = associatedMove;

            _isTransmissible = isTransmissible;
        }

        public bool Equal(IPassiveMove other)
        {
            if (other is not HealPassive ohp) return false;
            return (_associatedMove == ohp._associatedMove && _assignedPokemon == ohp._assignedPokemon && _healValue == ohp._healValue);
        }
        
        private void ApplyHeal()
        {
            if (_assignedPokemon.CurrentHp <= 0)
            {
                Debug.Log("nope");
                return;
            }
            _assignedPokemon.HpChange(_healValue);
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
                ApplyHeal();
                return false;
            }

            return true;
        }

        public void EndMove() {}
        
        public bool TransmitPassive(Pokemon newcomer)
        {
            EndMove();
            
            if (!_isTransmissible) return true;

            _assignedPokemon = newcomer;
            return false;
        }

        public void Application() {} //does nothing
    }
}
