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
    }
}
