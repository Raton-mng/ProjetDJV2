namespace Moves
{
    public class BuffPassive
    {
        private BuffNumber _buffs;
        private Pokemon _assignedPokemon;

        private int _turnsBeforeStart;
        private int _duration;

        public BuffPassive(BuffNumber buffs, Pokemon assignedPokemon)
        {
            _assignedPokemon = assignedPokemon;
            _buffs = buffs;
            
            _turnsBeforeStart = _buffs.turnsBeforeStart;
            _duration = buffs.duration;
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
            //le -1 correspond à ce que la fonciton est appelée juste après l'action du lanceur, même après le lancement de la capacité, donc pour garantir le bon fonctionnelement il faut mettre -1 comme threshold
            if (_turnsBeforeStart > -1)
            {
                _turnsBeforeStart -= 1;
                if (_turnsBeforeStart == -1) ApplyBuff(1);
                return false;
            }
            
            //si la fonction est appelée, c'est qu'il reste encore du duration car quand il tombe à zero, le buff est retiré de la liste.
            _duration -= 1;
            if (_duration == 0)
            {
                ApplyBuff(-1);
                return true;
            }

            return false;
        }
    }
}
