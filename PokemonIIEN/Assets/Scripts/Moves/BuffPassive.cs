namespace Moves
{
    public class BuffPassive : IPassiveMove
    {
        private BuffNumber _buffs;
        private Pokemon _assignedPokemon;

        private int _turnsBeforeStart;
        private int _duration;
        private bool _hasAppliedBuff; // sert pour quand _turnsBeforeStart est à 0 pour ne pas buffer 2 fois

        public BuffPassive(BuffNumber buffs, Pokemon assignedPokemon)
        {
            _assignedPokemon = assignedPokemon;
            _buffs = buffs;
            
            _turnsBeforeStart = _buffs.turnsBeforeStart;
            _duration = buffs.duration;
            _hasAppliedBuff = false;

            if (_turnsBeforeStart <= 0) ApplyBuff(1); //obligé de mettre ici pour les debuffs notamment
        }

        //endModifier vaut -1 quand c'est la fin de duration et qu'il faut enlever les buffs via cette fonction, et vaut 1 sinon
        private void ApplyBuff(int endModifier)
        {
            _assignedPokemon.BoostAttack(endModifier * _buffs.attackBuff);
            _assignedPokemon.BoostDefense(endModifier * _buffs.defenseBuff);
            _assignedPokemon.BoostSpeed(endModifier * _buffs.speedBuff);
            _hasAppliedBuff = true;
        }
        
        public bool DecrementDurations()
        {
            if (_turnsBeforeStart >= 0) //on compte 0 car le décrement est aussi activé juste après l'appel de la compétence, et on ne veut pas le compte, donc le décompté dure 1 tour de plus
            {
                _turnsBeforeStart -= 1;
                if (_turnsBeforeStart <= -1 && !_hasAppliedBuff) ApplyBuff(1);
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
            ApplyBuff(-1);
        }
    }
}
