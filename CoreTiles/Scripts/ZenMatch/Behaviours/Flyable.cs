using System;
using UnityEngine;
using ZenMatch.Utils;

namespace ZenMatch.Behaviours
{
    public class Flyable : MonoBehaviour
    {
        public bool Flying => _active;
        
        private Vector2 _startPos;
        private Vector2 _endPos;
        private float _flyTime;
        private float _timePassed;
        private bool _active;

        /// <summary>
        /// Экшн окончания полета.
        /// Вызывается раньше, чем закончится полет (для синка анимаций)
        /// </summary>
        private Action<Flyable> _endFlyCallback;

        public void Fly(Vector2 position, float flyTime, Action<Flyable> endFlyCallback = null)
        {
            _flyTime = flyTime;
            _endFlyCallback = endFlyCallback;
            _startPos = transform.position;
            _endPos = position;
            _timePassed = 0;
            _active = true;
        }

        public void ChangeTarget(GameObject newTarget)
        {
            // TODO
        }

        private void Update()
        {
            if (_active)
            {
                if (_timePassed < Configs.GameConfig.tileCollectFlyTime)
                {
                    float t = _timePassed / _flyTime;
                    var targetPos = Vector2.Lerp(_startPos, _endPos, t);
                    
                    SetPosition(targetPos);
                    
                    _timePassed += Time.deltaTime;
                    if (_timePassed > Configs.GameConfig.endFlyCallbackTime)
                    {
                        TryInvokeEndFlyCallback();
                    }
                }
                else
                {
                    TryInvokeEndFlyCallback();
                    SetPosition(_endPos);
                    _active = false;
                }
            }
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        }

        private void TryInvokeEndFlyCallback()
        {
            if (_endFlyCallback != null)
            {
                _endFlyCallback.Invoke(this);
                _endFlyCallback = null;
            }
        }
    }
}