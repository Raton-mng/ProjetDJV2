namespace Moves
{
    public interface IPassiveMove
    {
        public bool DecrementDurations();
        public void EndMove();
    }
}
