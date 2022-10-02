using UnityEngine;

namespace ZenMatch.Behaviours
{
    public class DelayedDestructible : MonoBehaviour
    {
        [Tooltip("Время жизни объекта")]
        public float lifeTime;
        
        private float _timePassed;

        private void Update()
        {
            _timePassed += Time.deltaTime;
            if (_timePassed > lifeTime)
            {
                Destroy(gameObject);
            }
        }
    }
}