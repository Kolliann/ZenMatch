using System;
using System.Collections;
using CoreTiles.Scripts.ZenMatch.Controllers;
using UnityEngine;
using UnityEngine.EventSystems;
using ZenMatch.Enums;
using ZenMatch.Interfaces;

namespace ZenMatch.Behaviours
{
    public class Clickable : MonoBehaviour, IHaveCallback<Clickable>,
        IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        public enum State
        {
            Pressed,
            Clicked,
            Unpressed
        }
        
        public State ClickState { get; private set; }
        
        private bool _interactable = true;
        private Action<Clickable> _callback;

        public void AddCallback(Action<Clickable> onTilesMatchedCallback)
        {
            _callback = onTilesMatchedCallback;
        }

        public void SetInteractable(bool interactable)
        {
            _interactable = interactable;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(!_interactable)
                return;
            ClickState = State.Pressed;
            _callback?.Invoke(this);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!_interactable)
                return;
            ClickState = State.Clicked;
            CoreGameController.SoundController.PlayActionSound(SoundActionType.TileClick);
            _callback?.Invoke(this);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            StartCoroutine(AwaitOneFrameOnPointerUp());
        }

        private IEnumerator AwaitOneFrameOnPointerUp()
        {
            yield return null;
            if(!_interactable)
                yield break;
            ClickState = State.Unpressed;
            _callback?.Invoke(this);
        }
    }
}