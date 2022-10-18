using _Scripts.Patterns.Core;
using UnityEngine;

namespace _Scripts.Patterns
{
    public class PooledObject : MonoBehaviour
    {
        public ePoolName poolName { get; set; }
        public bool isPooled { get; set; }
        
        public virtual void ResetObject()
        {
            EasyObjectPool.instance.ReturnObjectToPool(this, true);
        }
    }
}