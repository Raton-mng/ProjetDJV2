using System.Collections.Generic;

public enum PossibleTargets
{
    Me,
    SingleTarget,
    AllEnemies,
    All
}

public abstract class Move
{
    public PossibleTargets Targets;
    public Type Type;

    public abstract void DoSomething(List<Pokemon> targets);
}
