using System;
using UnityEngine;

namespace CoreTiles.Scripts.ZenMatch.Animatons
{
    public class GameWindowAnimator : MonoBehaviour
    {
        private Animator _animator;
        private Canvas _canvas;
        private Action _hideWindowCallback;
        private Action _showWindowCallback;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _canvas = GetComponent<Canvas>();
        }

        public void ShowGameWindow(Action callback)
        {
            _showWindowCallback = callback;
            _canvas.enabled = true;
            _animator.Play($"Show");
        }
        
        public void HideGameWindow(Action callback)
        {
            _hideWindowCallback = callback;
            _animator.Play($"Hide");
        }

        /// <summary>
        /// Triggered from hide animation
        /// </summary>
        public void OnWindowShowed()
        {
            _showWindowCallback?.Invoke();
        }
        
        /// <summary>
        /// Triggered from hide animation
        /// </summary>
        public void OnWindowHidden()
        {
            _canvas.enabled = false;
            _hideWindowCallback?.Invoke();
        }
    }
}