using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "Game/Type")]
public class Type : ScriptableObject
{
    public List<Type> strongAgainst;
    public List<Type> weakAgainst;
}
