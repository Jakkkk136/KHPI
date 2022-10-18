using UnityEngine;

namespace _Scripts.Patterns
{
    public class PooledFX : PooledObject
    {
        [Header("Settings")]
        [SerializeField] private float lifeTime = 2f;
        private float timeToLive;
    
        protected virtual void OnEnable()
        {
            timeToLive = Time.time + lifeTime;
        }

        private void Update()
        {
            if(Time.time >= timeToLive) ResetObject();
        }
    }
}
