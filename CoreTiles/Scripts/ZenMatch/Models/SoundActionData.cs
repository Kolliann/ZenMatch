using System;
using FMODUnity;
using ZenMatch.Enums;
using ZenMatch.Models.ScriptableObjects;

namespace ZenMatch.Models
{
    [Serializable]
    public class SoundActionData
    {
        public SoundActionType soundActionType;
        public EventReference audioEvent;
    }
    
    [Serializable]
    public class SoundFoodData
    {
        public TileModel food;
        public EventReference audioEvent;
    }
}