using System;
using System.Net.Mail;
using UnityEngine;

namespace _Scripts.Patterns
{
    public abstract class Singleton<T>: MonoBehaviour where T: Singleton<T>, new()
    {
        private static T sInstance = null;
		
        protected bool isCreatedOnLevel;
		
        public static T Instance
        {
            get
            {
                if (sInstance == null)
                {
                    sInstance = FindObjectOfType<T>(true);
                }
                
                if (sInstance == null)
                {
                    AutoCreateSingeltonAttribute createNewInstanceAttribute;

                    createNewInstanceAttribute =
                        (AutoCreateSingeltonAttribute) Attribute.GetCustomAttribute(
                            typeof(T), typeof(AutoCreateSingeltonAttribute));

                    if (createNewInstanceAttribute == null) return null;
                    
                    //Debug.LogWarning($"Create new instance of {typeof(T)}");
                    GameObject newGoForInstance = new GameObject($"{typeof(T)}_GameObject");
                    newGoForInstance.AddComponent<T>().isCreatedOnLevel = true;
                    sInstance = newGoForInstance.GetComponent<T>();
                }
			
                return sInstance;
            }

            protected set => sInstance = value;
        }

        public void ManualInit()
        {
            
        }
    }
}