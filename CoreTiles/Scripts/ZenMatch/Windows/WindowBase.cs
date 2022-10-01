using UnityEngine;

namespace ZenMatch.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        private Canvas _canvas;
        protected abstract void CustomAwake();
        protected void Show(bool show)
        {
            _canvas.enabled = show;
        }
        private void Awake()
        {
            _canvas = GetComponent<Canvas>();
            CustomAwake();
        }
    }
}