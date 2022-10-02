using System;
using UnityEngine;

namespace ZenMatch.Interfaces
{
    public interface ITilesCollector
    {
        GameObject gameObject { get; }
        event Action OnTilesFull;
    }
}