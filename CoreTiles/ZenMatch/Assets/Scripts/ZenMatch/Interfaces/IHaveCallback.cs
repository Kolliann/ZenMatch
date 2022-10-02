using System;
using Object = UnityEngine.Object;

namespace ZenMatch.Interfaces
{
    public interface IHaveCallback<out T> where T : Object
    {
        void AddCallback(Action<T> onTilesMatchedCallback);
    }
}