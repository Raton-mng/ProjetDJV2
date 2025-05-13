using UnityEngine.Events;

public abstract class MenuState
{
    public UnityEvent actionDone = new UnityEvent();

    public abstract void StartState();
    public abstract void EndState();
}
