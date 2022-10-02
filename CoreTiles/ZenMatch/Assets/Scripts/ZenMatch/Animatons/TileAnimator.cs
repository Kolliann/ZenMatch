using System;
using UnityEngine;

namespace ZenMatch.Animatons
{
    public class TileAnimator : MonoBehaviour
    {
        public Animator animator;
        
        private static readonly int PressedHash = Animator.StringToHash("pressed");
        private static readonly int InsertHash = Animator.StringToHash("insert");
        private static readonly int DestroyHash = Animator.StringToHash("destroy");

        private Action<TileAnimator> _tilePreInsertedCallback;
        private Action<TileAnimator> _tileInsertedCallback;
        private Action<TileAnimator> _tileDestroyedCallback;

        public void AnimatePress(bool pressed)
        {
            animator.SetBool(PressedHash, pressed);
        }

        /// <summary>
        /// Анимация вставки тайла в целевую панельку
        /// </summary>
        /// <param name="tilePreInsertedCallback"> Каллбек в середине анимации, когда элемент визуально "в нулях" </param>
        /// <param name="tileInsertedCallback"> Каллбек в конце анимации </param>
        public void AnimateInsert(Action<TileAnimator> tilePreInsertedCallback, Action<TileAnimator> tileInsertedCallback)
        {
            _tilePreInsertedCallback = tilePreInsertedCallback;
            _tileInsertedCallback = tileInsertedCallback;
            animator.SetTrigger(InsertHash);
        }
        
        /// <summary>
        /// Анимация уничтожения тайла при матче
        /// </summary>
        /// <param name="tileDestroyedCallback"> Каллбек в конце анимации </param>
        public void AnimateDestroy(Action<TileAnimator> tileDestroyedCallback = null)
        {
            _tileDestroyedCallback = tileDestroyedCallback;
            animator.SetTrigger(DestroyHash);
        }

        /// <summary>
        /// Triggered from animation
        /// </summary>
        public void OnTilePreInserted()
        {
            _tilePreInsertedCallback?.Invoke(this);
        }

        /// <summary>
        /// Triggered from animation
        /// </summary>
        public void OnTileInserted()
        {
            _tileInsertedCallback?.Invoke(this);
        }
        
        /// <summary>
        /// Triggered from animation
        /// </summary>
        public void OnTileDestroyed()
        {
            _tileDestroyedCallback?.Invoke(this);
        }
    }
}