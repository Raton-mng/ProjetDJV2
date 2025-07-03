namespace Moves
{
    public interface IPassiveMove
    {
        public bool TransmitPassive(Pokemon newcomer);
        public void Application();
        public bool Equal(IPassiveMove other);
        public bool DecrementDurations();
        public void EndMove();
    }
}
