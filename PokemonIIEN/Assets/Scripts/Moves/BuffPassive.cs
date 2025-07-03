namespace Moves
{
    public class BuffPassive : IPassiveMove
    {
        private bool _isTransmissible;
        private BuffNumber _buffs;
        private Pokemon _assignedPokemon;
        protected Move _associatedMove;

        private int _turnsBeforeStart;
        private int _duration;

        public BuffPassive(BuffNumber buffs, Pokemon assignedPokemon, Move associatedMove)
        {
            _assignedPokemon = assignedPokemon;
            _buffs = buffs;
            
            _turnsBeforeStart = _buffs.turnsBeforeStart;
            _duration = buffs.duration;

            _associatedMove = associatedMove;

            if (_turnsBeforeStart <= 0) _duration++;
            else _turnsBeforeStart++;

            _isTransmissible = buffs.isTransmissible;
        }
        
        public bool Equal(IPassiveMove other)
        {
            if (other is not BuffPassive obp) return false;
            return (_associatedMove == obp._associatedMove && _assignedPokemon == obp._assignedPokemon && _buffs == obp._buffs);
        }

        //endModifier vaut -1 quand c'est la fin de duration et qu'il faut enlever les buffs via cette fonction, et vaut 1 sinon
        private void ApplyBuff(int endModifier)
        {
            _assignedPokemon.BoostAttack(endModifier * _buffs.attackBuff);
            _assignedPokemon.BoostDefense(endModifier * _buffs.defenseBuff);
            _assignedPokemon.BoostSpeed(endModifier * _buffs.speedBuff);
        }
        
        public bool DecrementDurations()
        {
            if (_turnsBeforeStart > 0) //on compte 0 car le décrement est aussi activé juste après l'appel de la compétence, et on ne veut pas le compte, donc le décompté dure 1 tour de plus
            {
                _turnsBeforeStart -= 1;
                if (_turnsBeforeStart == 0) ApplyBuff(1);
                return false;
            }
            
            //si DecrementDurations() est appelée, c'est qu'il reste encore du duration car quand il tombe à zero, le buff est retiré de la liste.
            _duration -= 1;
            if (_duration <= 0)
            {
                ApplyBuff(-1);
                return true;
            }

            return false;
        }

        public void EndMove()
        {
            if (_turnsBeforeStart <= 0) ApplyBuff(-1);
        }

        public void Application()
        {
            if (_turnsBeforeStart <= 0)
            {
                ApplyBuff(1);
            }
        }

        public bool TransmitPassive(Pokemon newcomer)
        {
            EndMove();

            if (!_isTransmissible) return false;

            _assignedPokemon = newcomer;
            Application();
            return true;
        }
    }
}
