using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using UnityEngine;
using ZenMatch.Enums;
using ZenMatch.Models;
using ZenMatch.Models.ScriptableObjects;

namespace ZenMatch.Controllers
{
    public class SoundController : MonoBehaviour
    {
        public List<SoundActionData> actionSounds;
        public List<SoundFoodData> foodSounds;

        public void PlayActionSound(SoundActionType actionType)
        {
            var eventRef = GetFmodEventForAction(actionType);
            RuntimeManager.PlayOneShot(eventRef);
        }
        
        public void PlayFoodSound(TileModel foodTile)
        {
            var eventRef = GetFmodEventForFood(foodTile);
            RuntimeManager.PlayOneShot(eventRef);
        }
        
        private EventReference GetFmodEventForAction(SoundActionType actionType)
        {
            var audioEventData = actionSounds.FirstOrDefault(eventData => eventData.soundActionType == actionType);
            if (audioEventData != null) 
                return audioEventData.audioEvent;
            return new EventReference();
        }
        
        private EventReference GetFmodEventForFood(TileModel tileModel)
        {
            var audioEventData = foodSounds.FirstOrDefault(eventData => eventData.food == tileModel);
            if (audioEventData != null) 
                return audioEventData.audioEvent;
            return new EventReference();
        }
    }
    
}