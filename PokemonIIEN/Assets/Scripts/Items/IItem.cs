using System;
using UnityEngine;

namespace Items
{
    [Serializable]
    public abstract class PokeItem : ScriptableObject
    {
        public string itemName;
    }
}
