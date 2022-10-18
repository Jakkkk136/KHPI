using UnityEngine;
using UnityEngine.Events;

namespace _Scripts.Patterns.Events.OnUnityEvents
{
    public abstract class OnUnityEventBase : MonoBehaviour
    {
        [SerializeField] protected UnityEvent eventToCall;
    }
}