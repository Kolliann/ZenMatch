using System;

namespace ZenMatch.Interfaces
{
    public interface ITilesOwner
    {
        int TilesCount { get; }
        event Action OnTilesCountChange;
    }
}