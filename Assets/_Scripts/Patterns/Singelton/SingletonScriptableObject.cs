using System.Linq;
using _Scripts.Utils;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts.Patterns
{
    public abstract class SingletonScriptableObject<T> : SerializedScriptableObject where T : SerializedScriptableObject
    {
        static T instance;
        public static T Instance 
        {
            get 
            {
                if (instance != null) return instance;
                
                instance = Resources.Load<T>(typeof(T).Name);

#if UNITY_EDITOR
                if (instance == null)
                {
                    instance = ScriptableObjectUtils.GetAllInstances<T>().First();
                }
#endif
                (instance as SingletonScriptableObject<T>)?.OnInitialize();
                
                return instance;
            }
        }
        
        // Optional overridable method for initializing the instance.
        protected virtual void OnInitialize() { }
    }
}