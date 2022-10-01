using UnityEngine;

namespace ZenMatch.Windows
{
    public class WindowsManager : MonoBehaviour
    {
        [SerializeField, Tooltip("Windows")]
        private GameObject[] windows;

        private static WindowsManager _instance;

        public static WindowsManager Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;
                _instance = FindObjectOfType<WindowsManager>();
                return _instance != null ? _instance : null;
            }
        }

        /// <summary>
        /// Return window by type
        /// </summary>
        public T GetWindow<T>() where T : Component
        {
            foreach (var window in windows)
            {
                var component = window.GetComponent<T>();
                if (!component) 
                    continue;

                return component;
            }
            return null;
        }
    }

}